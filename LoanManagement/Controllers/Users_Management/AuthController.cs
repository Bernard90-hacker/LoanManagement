using LoanManagement.core;
using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;

namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class AuthController : Controller
	{
		private readonly IUtilisateurService _utilisateurService;
		private readonly IDepartmentService _departmentService;
		private readonly IMotDePasseService _motDePasseService;
		private readonly IParamMotDePasseService _paramMotDePasseService;
		private readonly IJournalService _journalService;
		private readonly JournalisationService _journalisationService;
		private readonly ITypeJournalService _typeJournalService;
		private readonly IEmployeService _employeService;
		private readonly IProfilService _profilService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly IDirectionService _directionService;

        public AuthController(UtilisateurService utilisateurService, ILoggerManager logger,
			IMapper mapper, IMotDePasseService motDePasseService,
			IProfilService profilService, IParamMotDePasseService param,
			IEmployeService employeService, IJournalService journalService, 
			IDepartmentService department, IDirectionService directionService,
			ITypeJournalService typeJournalService, JournalisationService journalisationService)
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
		}



		[HttpPost("login")]
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
				var cookieOptions = new CookieOptions()
				{
					Expires = DateTime.Now.AddHours(5)
				};
				Response.Cookies.Append("Username", user.Username, cookieOptions);
				await _utilisateurService.Connect(user);
				var journal = new Journal() { Libelle = "Connexion d'un utilisateur" };
				await _journalisationService.Journalize(journal);
				return Ok(new
				{
					Email = employe.Email,
					Nom = employe.Nom,
					Prenoms = employe.Prenoms,
					CodeProfil = profil.Code,
					DateExpirationProfil = profil.DateExpiration,
					CodeDepartement = departement.Code,
					Direction = direction.Code,
					Username = user.Username
				});
			}
			catch (Exception ex)
			{
				
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("logout")]
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
				string? value = string.Empty;
				Request.Cookies.TryGetValue("Username", out value);
				Response.Cookies.Delete("Username");
				var journal = new Journal() { Libelle = "Déconnexion d'un utilisateur" };
				await _journalisationService.Journalize(journal);

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
