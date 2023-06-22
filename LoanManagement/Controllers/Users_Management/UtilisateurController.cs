using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class UtilisateurController : Controller
	{
		private readonly IUtilisateurService _utilisateurService;
		private readonly IMotDePasseService _motDePasseService;
		private readonly IParamMotDePasseService _paramMotDePasseService;
		private readonly IProfilService _profilService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly IConfiguration _config;
		public UtilisateurController(UtilisateurService utilisateurService, ILoggerManager logger,
			IMapper mapper, IMotDePasseService motDePasseService, IConfiguration config, 
			IProfilService profilService, IParamMotDePasseService param)
		{
			_utilisateurService = utilisateurService;
			_logger = logger;
			_mapper = mapper;
			_motDePasseService = motDePasseService;
			_config = config;
			_profilService = profilService;
			_paramMotDePasseService = param;
		}

		[HttpPost("register")]
		public async Task<ActionResult<UtilisateurRessource>> Register(UtilisateurRessource ressource)
		{
			try
			{
				var validator = new UtilisateurValidator();
				var validationResult = await validator.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Enregistrement d'un utilisateur : champs obligatoires");
					return BadRequest();
				}
				var param = await _paramMotDePasseService.GetCurrentParameter();
				if(param is null)
				{
					_logger.LogWarning("Enregistrement d'un utilisateur : Aucun paramétrage des mots de passe n'a été effectué, veuillez contacter l'équipe d'assistance");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramétrage des mots de passe n'a été effectué, veuillez contacter l'équipe d'assistance"));
				}
				var doesPasswordMatchAllRequirements = await _utilisateurService.DidPasswordMatchAllRequirements(param, ressource.Password, ressource.Username);
				if (!doesPasswordMatchAllRequirements)
				{
					_logger.LogWarning("Enregistrement d'un utilisateur : Le mot de passe entré ne suit pas la convention fixée par l'administrateur");
					return BadRequest();
				}
				var utilisateur = _mapper.Map<Utilisateur>(ressource);
				string hash = string.Empty;
				byte[] salt;
				Constants.Utils.UtilsConstant.HashPassword(ressource.Password, out salt, out hash);
				utilisateur.PasswordHash = hash;
				utilisateur.PasswordSalt = salt;
				utilisateur.DateAjout = DateTime.Now.ToString("dd/MM/yyyy");
				utilisateur.DateModificationMotDePasse = DateTime.Now.ToString("dd/MM/yyyy");
				string refreshTokenTime = string.Empty;
				utilisateur.RefreshToken = Constants.Utils.UtilsConstant.CreateRefreshToken(utilisateur.Username, _config.GetSection("JWT:RefreshToken").Value, out refreshTokenTime);
				utilisateur.RefreshTokenTime = refreshTokenTime;

				var utilisateurCreated = await _utilisateurService.Create(utilisateur);
				//Ensuite on sauvegarde les informations dans le service de mot de passe:
				var motDePasseRessource = new MotDePasseRessource()
				{
					OldPasswordHash = hash,
					OldPasswordSalt = salt,
					DateAjout = DateTime.Now.ToString("dd/MM/yyyy"),
					UtilisateurId = utilisateurCreated.Id
				};
				var motDepasse = _mapper.Map<MotDePasse>(motDePasseRessource);
				//Mappage en vue de retourner la ressource à l'utilisateur
				var utilisateurResult = _mapper.Map<UtilisateurRessource>(utilisateurCreated);
				var motDePasseCreated = await _motDePasseService.Create(motDepasse);
				_logger.LogInformation("Enregistrement d'un utilisateur : Opération effectuée avec succès");
				return Ok(utilisateurResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("Passwords configuration")]
		public async Task<ActionResult<string>> GetPasswordsConfiguration()
		{
			try
			{
				var config = await _utilisateurService.GetPasswordConfiguration();
				if (config is null)
				{
					_logger.LogWarning("'Détails des configurations des mots de passe' : Aucune configuration faite");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucune configuration faite"));
				}
				_logger.LogInformation($"'Configuration des mots de passe': Opération effectuée avec succès");
				return Ok(config);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}
		[HttpGet("all")]
		public async Task<ActionResult<PagedList<GetUtilisateurRessource>>> GetAll([FromQuery] UtilisateurParameters parameters)
		{
			try
			{
				var users = await _utilisateurService.GetAll(parameters);
				if (users is null)
				{
					_logger.LogWarning("'Détails des utilisateurs' : Aucun utilisateur trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}
				var userResults = _mapper.Map<IEnumerable<GetUtilisateurRessource>>(users);
				var metadata = new
				{
					users.PageSize,
					users.CurrentPage,
					users.TotalCount,
					users.TotalPages,
					users.HasNext,
					users.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation($"'Liste des utilisateurs ': Opération effectuée avec succès, {users.Count} utilisateurs retournés");
				return Ok(userResults);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<PagedList<GetUtilisateurRessource>>> GetUserById(int id)
		{
			try
			{
				var user = await _utilisateurService.GetUserById(id);
				if (user is null)
				{
					_logger.LogWarning("'Détails d'un utilisateur' : Aucun utilisateur trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}
				var userResult = _mapper.Map<GetUtilisateurRessource>(user);

				_logger.LogInformation($"'Détails d'un utilisateur ': Opération effectuée avec succès");
				return Ok(userResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{username}")]
		public async Task<ActionResult<PagedList<GetUtilisateurRessource>>> GetUserByUsername(string username)
		{
			try
			{
				var user = await _utilisateurService.GetUserByUsername(username);
				if (user is null)
				{
					_logger.LogWarning("'Détails d'un utilisateur' : Aucun utilisateur trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}
				var userResult = _mapper.Map<GetUtilisateurRessource>(user);

				_logger.LogInformation($"'Détails d'un utilisateur ': Opération effectuée avec succès");
				return Ok(userResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<PagedList<GetUtilisateurRessource>>> Delete(int id)
		{
			try
			{
				var user = await _utilisateurService.GetUserById(id);
				if (user is null)
				{
					_logger.LogWarning("'Détails d'un utilisateur' : Aucun utilisateur trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}
				await _utilisateurService.DeleteUtilisateur(user);

				_logger.LogInformation($"'Détails d'un utilisateur ': Opération effectuée avec succès");
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPatch("updatePassword")]
		public async Task<ActionResult<PagedList<GetUtilisateurRessource>>> UpdatePassword(UserUpdateRessource ressource)
		{
			try
			{
				var userDb = await _utilisateurService.GetUserById(ressource.UserId);
				var users = await _utilisateurService.GetAll();
				var isPasswordAlreadyUsed = false;
				string hash;
				byte[]? salt;
				if (userDb is null) //On vérifie s'il existe un utilisateur avec cet id
				{
					_logger.LogWarning("'Détails d'un utilisateur' : Aucun utilisateur trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}

				if (!Constants.Utils.UtilsConstant.CheckPassword(ressource.OldPassword, userDb.PasswordHash, userDb.PasswordSalt)) //On compare le mot de passe avec celui de l'utilisateur
				{
					_logger.LogWarning("'Mise à jour d'un utilisateur' : Mot de passe incorrect");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun utilisateur ayant ce mot de passe n'a été trouvé"));
				}
				var param = await _paramMotDePasseService.GetCurrentParameter();
				if (param is null)
				{
					_logger.LogWarning("Enregistrement d'un utilisateur : Aucun paramétrage des mots de passe n'a été effectué, veuillez contacter l'équipe d'assistance");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramétrage des mots de passe n'a été effectué, veuillez contacter l'équipe d'assistance"));
				}
				var doesPasswordMatchAllRequirements = await _utilisateurService.DidPasswordMatchAllRequirements(param, ressource.NewPassword, userDb.Username);
				if (!doesPasswordMatchAllRequirements)
				{
					_logger.LogWarning("Enregistrement d'un utilisateur : Le mot de passe entré ne suit pas la convention fixée par l'administrateur");
					return BadRequest();
				}
				var user = _mapper.Map<Utilisateur>(ressource);
				//Vérifier si le mot de passe entré par l'utilisateur a été utilisé
				isPasswordAlreadyUsed = await _utilisateurService.CheckIfPasswordHasBeenEverUsedByUser(ressource.UserId, ressource.NewPassword);
				//Si oui, on renvoie une erreur à l'utilisateur.

				if (isPasswordAlreadyUsed)
				{
					_logger.LogWarning("'Mise à jour d'un utilisateur' : Mot de passe déjà utilisé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_ALREADY_EXISTS, description: "Ce mot de passe a déjà été utilisé, veuillez le changer"));
				}

				Constants.Utils.UtilsConstant.HashPassword(ressource.NewPassword, out salt, out hash);

				user.PasswordHash = hash;
				user.PasswordSalt = salt;
				user.DateModificationMotDePasse = DateTime.Now.ToString("dd/MM/yyyy");

				var motDePasseRessource = new MotDePasseRessource()
				{
					OldPasswordHash = hash,
					OldPasswordSalt = salt,
					UtilisateurId = ressource.UserId,
					DateAjout = DateTime.Now.ToString("dd/MM/yyyy")
				};

				var userUpdated = await _utilisateurService.Update(userDb, user); //On fait une mise à jour de l'utilisateur
				var result = _mapper.Map<GetUtilisateurRessource>(userUpdated);
				var motDePasseDb = _mapper.Map<MotDePasse>(motDePasseRessource);
				var motDepasse = _motDePasseService.Create(motDePasseDb); //On enregistre le nouveau mot de passe dans sa collection de mots de passe.
				_logger.LogInformation($"'Détails d'un utilisateur ': Opération effectuée avec succès");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPatch("update")]
		public async Task<ActionResult<GetUtilisateurRessource>> UpdateUserAccountExpiryDate(UpdateUserAccountExpiryDateRessource ressource)
		{
			try
			{
				var user = await _utilisateurService.GetUserById(ressource.UserId);
				if(user is null)
				{
					_logger.LogWarning("'Détails d'un utilisateur' : Aucun utilisateur trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}

				await _utilisateurService.UpdateUserAccountExpiryDate(user, ressource.ExpiryDate);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPatch("desactivate")]
		public async Task<ActionResult<GetUtilisateurRessource>> DesactivateAccount(int userId)
		{
			try
			{
				var user = await _utilisateurService.GetUserById(userId);
				if (user is null)
				{
					_logger.LogWarning("'Détails d'un utilisateur' : Aucun utilisateur trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}

				await _utilisateurService.DesactivateUserAccount(user);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPatch("activate")]
		public async Task<ActionResult<GetUtilisateurRessource>> ActivateUserAccount(int userId)
		{
			try
			{
				var user = await _utilisateurService.GetUserById(userId);
				if (user is null)
				{
					_logger.LogWarning("'Détails d'un utilisateur' : Aucun utilisateur trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}

				await _utilisateurService.ActivateUserAccount(user);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("activatedAccounts")]
		public async Task<ActionResult<PagedList<GetUtilisateurRessource>>> GetActivatedAccounts()
		{
			try
			{
				var users = await _utilisateurService.GetActivatedAccounts();
				if (users is null)
				{
					_logger.LogWarning("'Liste des comptes utilisateurs actifs' : Aucun utilisateur trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}
				var userResults = _mapper.Map<IEnumerable<GetUtilisateurRessource>>(users);
				var metadata = new
				{
					users.PageSize,
					users.CurrentPage,
					users.TotalCount,
					users.TotalPages,
					users.HasNext,
					users.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation($"'Liste des comptes utilisateurs actifs': Opération effectuée avec succès, {users.Count} utilisateurs retournés");
				return Ok(userResults);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("disabledAccounts")]
		public async Task<ActionResult<PagedList<GetUtilisateurRessource>>> GeDisabledAccounts()
		{
			try
			{
				var users = await _utilisateurService.GetDisableAccounts();
				if (users is null)
				{
					_logger.LogWarning("'Liste des comptes utilisateurs inactifs' : Aucun utilisateur trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}
				var userResults = _mapper.Map<IEnumerable<GetUtilisateurRessource>>(users);
				var metadata = new
				{
					users.PageSize,
					users.CurrentPage,
					users.TotalCount,
					users.TotalPages,
					users.HasNext,
					users.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation($"'Liste des comptes utilisateurs actifs': Opération effectuée avec succès, {users.Count} utilisateurs retournés");
				return Ok(userResults);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{id}/profil")]
		public async Task<ActionResult<ProfilRessource>> GetUtilisateurProfil(int id)
		{
			try
			{
				var user = await _utilisateurService.GetUserById(id);
				if (user is null)
				{
					_logger.LogWarning("'Profil utilisateur' : Aucun utilisateur trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}
				var profil = await _profilService.GetUserProfil(user);
				var profilResult = _mapper.Map<ProfilRessource>(profil);

				_logger.LogInformation($"'Profil d'un utilisateur': Opération effectuée avec succès");
				return Ok(profilResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

	}
}
