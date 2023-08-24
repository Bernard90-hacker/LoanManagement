using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using Microsoft.AspNetCore.Http.Extensions;

namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class ApplicationController : Controller
	{
		private readonly IApplicationService _applicationService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly IJournalService _journalService;
		private readonly JournalisationService _journalisationService;
		private readonly IUtilisateurService _utilisateurService;
		private readonly ITypeJournalService _typeJournalService;
        public ApplicationController(IApplicationService applicationService, 
			IMapper mapper, ILoggerManager logger, IJournalService journalService,
			IUtilisateurService utilisateurService, ITypeJournalService typeJournalService,
			JournalisationService journalisationService)
        {
			_applicationService = applicationService;
			_mapper = mapper;
			_logger = logger;
			_journalService = journalService;
			_utilisateurService = utilisateurService;
			_typeJournalService = typeJournalService;
			_journalisationService = journalisationService;
        }

		[HttpGet("all")]
		public async Task<ActionResult<PagedList<GetApplicationRessource>>> GetAll([FromQuery] ApplicationParameters parameters)
		{
			var Journal = new Journal() { Libelle = "Liste des modules", TypeJournalId = 8, Entite = "User"};
			try
			{
				var all = await _applicationService.GetAll(parameters);
				if (all is null)
				{
					_logger.LogWarning("'Détails module : Aucun module n'a pas été trouvée");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Departement(s) non trouvée(s)"));
				}
				
				var allResult = _mapper.Map<IEnumerable<GetApplicationRessource>>(all);
				var metadata = new
				{
					all.PageSize,
					all.CurrentPage,
					all.TotalCount,
					all.TotalPages,
					all.HasNext,
					all.HasPrevious
				};

				
				if (Request.Headers.ContainsKey("X-Journalisation"))
				{
					var x = Request.Headers["X-Journalisation"];
					Journal = JsonConvert.DeserializeObject<Journal>(x);
				}
				Journal.Niveau = 2; //SUCCES
				await _journalisationService.Journalize(Journal);
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation($"'Liste des modules ': Opération effectuée avec succès, {all.Count} modules retournés");
				return Ok(allResult);
			}
			catch (Exception ex)
			{
				Journal.Niveau = 1; //ECHEC
				await _journalisationService.Journalize(Journal);
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<GetApplicationRessource>> GetApplicationById(int id)
		{
			try
			{
				var app = await _applicationService.GetApplicationById(id);
				if (app is null)
				{
					_logger.LogWarning($"'Détails d'un module :  Module inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Module inexistant"));
				}

				var appRessource = _mapper.Map<GetApplicationRessource>(app);

				_logger.LogInformation("Opération effectuée avec succès");
				var journalisation = new Journal() { Libelle = "Récupération d'un module par son id" };

				return Ok(appRessource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.NOT_FOUND, title: "Ressource non trouvée", detail: ex.Message);
			}
		}


		[HttpGet("{code}")]
		public async Task<ActionResult<ApplicationRessource>> GetApplicationByCode(string code)
		{
			try
			{
				var app = await _applicationService.GetApplicationByCode(code);
				if (app is null)
				{
					_logger.LogWarning($"'Détails d'un module :  Module inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Module inexistant"));
				}

				var appRessource = _mapper.Map<GetApplicationRessource>(app);

				_logger.LogInformation("Opération effectuée avec succès");
				var journalisation = new Journal() { Libelle = "Identification d'un module par son code" };
				return Ok(appRessource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.NOT_FOUND, title: "Ressource non trouvée", detail: ex.Message);
			}
		}

		[HttpGet("{code}/sousModules")]
		public async Task<ActionResult<GetApplicationRessource>> GetApplicationSousModules(string code)
		{
			try
			{
				var app = await _applicationService.GetApplicationByCode(code);
				if (app is null)
				{
					_logger.LogWarning($"'Détails d'un module :  Module inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Module inexistant"));
				}
				var sousModules = await _applicationService.GetApplicationModules(app.Id);

				var appRessource = _mapper.Map<IEnumerable<GetApplicationRessource>>(sousModules);

				_logger.LogInformation($"Liste des sous-modules du module {app.Code} : Opération effectuée avec succès");
				var journal = new Journal() { Libelle = "Listes des sous-modules d'un module" };
				await _journalisationService.Journalize(journal);

				return Ok(appRessource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.NOT_FOUND, title: "Ressource non trouvée", detail: ex.Message);
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult<GetApplicationRessource>> Add(SaveApplicationRessource ressource)
		{
			try
			{
				//Validation
				var validation = new SaveApplicationRessourceValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				var app = _mapper.Map<Application>(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Enregistrement d'un module : champs obligatoires");
					return BadRequest();
				}
				if (ressource.ApplicationCode != string.Empty)
				{
					var module = await _applicationService.GetApplicationByCode(ressource.ApplicationCode);
					if (module is null)
					{
						_logger.LogWarning("Enregistrement d'un module : Le sous-module renseigné n'existe pas");
						return BadRequest("Le sous-module renseigné n'existe pas");
					}
					app.ApplicationId = module.Id;
				}
				else app.ApplicationId = null;
				
				//Configuration du code
				var apps = (await _applicationService.GetAll());
				string reference = string.Empty;
				if (apps.Count() == 0)
					reference = "00000";
				else
					reference = apps.LastOrDefault().Code.Substring(9);
				app.Code = "MODULE - " + Constants.Utils.UtilsConstant.IncrementStringWithNumbers(reference);

				var appCodeIsAlreadyUsed = await _applicationService.GetApplicationByCode(app.Code);
				if (appCodeIsAlreadyUsed is not null)
				{
					_logger.LogWarning("Enregistrement d'un module : champs obligatoires");
					return BadRequest();
				}
				//Enregistrement
				var appCreated = await _applicationService.Create(app);

				//Mappage en vue de retourner la ressource à l'utilisateur
				var appResult = _mapper.Map<GetApplicationRessource>(appCreated);

				_logger.LogInformation("Enregistrement d'une application : Opération effectuée avec succès");
				var journalisation = new Journal() { Libelle = "Enregistrement d'un module" };
				await _journalisationService.Journalize(journalisation);
				return Ok(appResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("updateVersion")]
		public async Task<ActionResult<GetApplicationRessource>> UpdateVersion(UpdateApplicationVersionRessource ressource)
		{
			try
			{
				//Validation
				var validation = new UpdateApplicationVersionValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Modification d'un module : champs obligatoires");
					return BadRequest();
				}	
				var application = await _applicationService.GetApplicationByCode(ressource.ApplicationCode);
				if (application is null)
				{
					_logger.LogWarning("Modification d'un module : Le code renseigné n'existe pas");
					return BadRequest("Le code renseigné n'existe pas");
				}
				//Mise à jour
				var appUpdated = await _applicationService.UpdateVersion(application, ressource.Version);

				//Mappage en vue de retourner la ressource à l'utilisateur
				var appResult = _mapper.Map<GetApplicationRessource>(appUpdated);

				_logger.LogInformation("Modification d'un module : Opération effectuée avec succès");
				var journal = new Journal() { Libelle = "Mise à jour de la version d'un module" };
				await _journalisationService.Journalize(journal);
				return Ok(appResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("updateStatus")]
		public async Task<ActionResult<GetApplicationRessource>> UpdateStatus(UpdateApplicationStatusRessource ressource)
		{
			try
			{
				//Validation
				var validation = new UpdateApplicationStatusValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Modification d'un module : champs obligatoires");
					return BadRequest();
				}
				var application = await _applicationService.GetApplicationByCode(ressource.ApplicationCode);
				if (application is null)
				{
					_logger.LogWarning("Modification d'un module : Le code renseigné n'existe pas");
					return BadRequest("Le code renseigné n'existe pas");
				}
				//Mise à jour
				var appUpdated = await _applicationService.UpdateStatus(application, ressource.Status);

				//Mappage en vue de retourner la ressource à l'utilisateur
				var appResult = _mapper.Map<GetApplicationRessource>(appUpdated);

				_logger.LogInformation("Modification du statut d'un module : Opération effectuée avec succès");
				var journal = new Journal() { Libelle = "Modification du statut d'un module" };
				await _journalisationService.Journalize(journal);
				return Ok(appResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpDelete("{code}")]
		public async Task<ActionResult<ApplicationRessource>> Delete(string code)
		{
			try
			{
				var application = await _applicationService.GetApplicationByCode(code); //Dans notre cas, si le applicationId est renseigné, cela veut dire que c'est un sous-module et qu'il a pour module parent l'id de l'applicationId
				if(application is null)
				{
					_logger.LogWarning("Suppression d'un module : Le module renseigné n'existe pas");
					return BadRequest("Le module renseigné n'existe pas");
				}
				var sousModules = await _applicationService.GetApplicationModules(application.Id);
				if(sousModules.Count() != 0)
				{
					_logger.LogWarning("Suppression d'un module : Impossible d'effectuer cette action, veuillez supprimer les sous-modules d'abord");
					return BadRequest("Impossible d\'effectuer cette action, veuillez supprimer les sous-modules d\'abord");
				}
				await _applicationService.Delete(application);
				var journal = new Journal() { Libelle = "Suppression d'un module" };
				await _journalisationService.Journalize(journal);
				_logger.LogWarning("Suppression d'un module : Opération effectuée avec succès");
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
