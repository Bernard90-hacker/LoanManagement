using LoanManagement.core;
using LoanManagement.core.Services.Users_Management;
using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;
using System.Security.Claims;

namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class AuthController : Controller
	{
		private readonly IUtilisateurService _utilisateurService;
		private readonly IDepartmentService _departmentService;
		private readonly IMotDePasseService _motDePasseService;
		private readonly IParamGlobalService _paramMotDePasseService;
		private readonly IJournalService _journalService;
		private readonly JournalisationService _journalisationService;
		private readonly ITypeJournalService _typeJournalService;
		private readonly IEmployeService _employeService;
		private readonly IProfilService _profilService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly IDirectionService _directionService;
		private readonly EmailService _mailService;
		private readonly TokenService _tokenService;
		private readonly IConfiguration _config;
        public AuthController(UtilisateurService utilisateurService, ILoggerManager logger,
			IMapper mapper, IMotDePasseService motDePasseService,
			IProfilService profilService, IParamGlobalService param,
			IEmployeService employeService, IJournalService journalService, 
			IDepartmentService department, IDirectionService directionService,
			ITypeJournalService typeJournalService, JournalisationService journalisationService,
			EmailService mailService, TokenService tokenService, IConfiguration conf)
        {
			_utilisateurService = utilisateurService;
			_logger = logger;
			_mapper = mapper;
			_motDePasseService = motDePasseService;
			_profilService = profilService;
			_paramMotDePasseService = param;
			_employeService = employeService;
			_journalService = journalService;
			_departmentService = department;
			_directionService = directionService;
			_typeJournalService = typeJournalService;
			_journalisationService = journalisationService;
			_mailService = mailService;
			_tokenService = tokenService;
			_config = conf;
		}



		[HttpPost("Login")]
		public async Task<ActionResult> Login(LoginRessource ressource)
		{
			try
			{
				var validation = new LoginRessourceValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Connexion d'un utilisateur : Champs incorrects");
					return BadRequest();
				}
				var user = await _utilisateurService.GetUserByUsername(ressource.Username);
				if(user is null)
				{
					_logger.LogWarning("Connexion d'un utilisateur : Nom d'utilisateur incorrect");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Nom d'utilisateur incorrect"));
				}
				var isPasswordCorrect = Constants.Utils.UtilsConstant.CheckPassword(ressource.Password,
					user.PasswordHash, user.PasswordSalt);
				if (!isPasswordCorrect)
				{
					_logger.LogWarning("Connexion d'un utilisateur : Mot de passe incorrect");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Mot de passe incorrect"));
				}
				var dateExpirationCompte = DateTime.ParseExact(user.DateExpirationCompte, "dd/MM/yyyy", CultureInfo.CurrentCulture);
				if(DateTime.Now >= dateExpirationCompte)
				{
					_logger.LogWarning("Connexion d'un utilisateur : Compte expiré");
					return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Compte expiré"));
				}
				var param = await _paramMotDePasseService.GetCurrentParameter();
				var dateAjout = DateTime.ParseExact(user.DateAjout, "dd/MM/yyyy", CultureInfo.CurrentCulture);
				var currentParam = await _paramMotDePasseService.GetCurrentParameter();
				if((dateAjout.AddMonths(currentParam.DelaiExpiration) <= DateTime.Now))
				{
					_logger.LogWarning("Connexion d'un utilisateur : Le délai de votre mot de passe a expiré, veuillez le modifier");
					return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, description : "Délai de mot de passe expiré"));
				}
				var employe = await _employeService.GetEmployeByUsername(user.Username);
				var profil = await _utilisateurService.GetUserProfil(user);
				var departement = await _departmentService.GetDepartmentById(employe.DepartementId);
				var direction = await _directionService.GetDirectionById(departement.DirectionId);
				if(DateTime.Now >= DateTime.ParseExact(profil.DateExpiration, "dd/MM/yyyy", CultureInfo.CurrentCulture))
				{
					_logger.LogWarning("Connexion d'un utilisateur : Profil expiré");
					return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Profil expiré"));
				}
				await _utilisateurService.Connect(user);
				var journal = new Journal() { Libelle = "Connexion d'un utilisateur", UtilisateurId = user.Id, TypeJournalId = 1};
				string role = string.Empty;
				if (profil.Id == 1)
					role = "Admin";
				else
					role = "User";
				journal.Entite = role;
				var claims = new List<Claim>()
				{
					new Claim(ClaimTypes.Role, role)
				};
				var token = _tokenService.GenerateAccessToken(_config.GetSection("JWT:Key").Value, claims,
					_config.GetSection("JWT:Issuer").Value, _config.GetSection("JWT:Audience").Value);
				await _journalisationService.Journalize(journal);
				CookieOptions cookieOptions = new();
				cookieOptions.Expires = DateTime.Now.AddHours(2);
				cookieOptions.HttpOnly = true; //This will make the cookies only accessible by the backend.
				Response.Cookies.Append("token", token, cookieOptions);

				
				return Ok(new
				{
					Email = employe.Email,
					Nom = employe.Nom,
					Prenoms = employe.Prenoms,
					CodeProfil = profil.Code,
					DateExpirationProfil = profil.DateExpiration,
					CodeDepartement = departement.Code,
					Direction = direction.Code,
					Username = user.Username,
					Token = token
				});
			}
			catch (Exception ex)
			{
				
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("Logout")]
		public async Task<ActionResult> Logout(string username)
		{
			try
			{
				var user = await _utilisateurService.GetUserByUsername(username);
				if(user is null)
				{
					_logger.LogWarning("Déconnexion d'un utilisateur : Nom d'utilisateur inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur inexistant"));
				}
				await _utilisateurService.Disconnect(user);
				Response.Cookies.Delete("token");
				var journal = new Journal() { Libelle = "Déconnexion d'un utilisateur", TypeJournalId = 2, Niveau = 2 };
				await _journalisationService.Journalize(journal);

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("ForgotPassword")]
		public async Task<ActionResult> Forgot(string username)
		{
			try
			{
				var user = await _utilisateurService.GetUserByUsername(username);
				if(user is null)
				{
					_logger.LogWarning("Oubli du mot de passe : Nom d'utilisateur inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur inexistant"));
				}
				var currentParam = await _paramMotDePasseService.GetCurrentParameter();
				var generatedPassword = Constants.Utils.UtilsConstant.GeneratePassword(currentParam.Taille, 1);
				string hash = string.Empty;
				byte[] salt;
				var newUser = new Utilisateur()
				{
					PasswordHash = Constants.Utils.UtilsConstant.HashPassword(generatedPassword, out salt, out hash),
					PasswordSalt = salt,
					Username = user.Username,
					DateModificationMotDePasse = DateTime.Now.ToString("dd/MM/yyyy")
				};
				var userUpdated = await _utilisateurService.Update(user, newUser);
				var employe = await _employeService.GetEmployeByUsername(user.Username);
				try
				{
					_mailService.SendPasswordResetMailAsync(generatedPassword, employe.Email);
				}
				catch (Exception)
				{
					_logger.LogError("Une erreur est survenue pendant l'envoi du mail : Oubli de mot de passe");
					return BadRequest("Une erreur est survenue pendant l'envoi du mail");
				}
				_logger.LogInformation("Oubli de mot de passe : Opération effectuée avec succès");
				return Ok();

			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}
    }
}
