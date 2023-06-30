using LoanManagement.core.Models.Users_Management;

namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class HabilitationProfilController : Controller
	{
		private readonly IHabilitationProfilService _habilitationProfilService;
		private readonly IProfilService _profilService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;


		public HabilitationProfilController(IHabilitationProfilService habilitationProfilService, 
			ILoggerManager logger, IMapper mapper, IProfilService profilService)
        {
			_logger = logger;
			_mapper = mapper;
			_habilitationProfilService = habilitationProfilService;
			_profilService = profilService;
        }

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<HabilitationProfilRessource>>> GetAll([FromQuery] HabilitationProfilParameters parameters)
		{
			try
			{
				var droits = await _habilitationProfilService.GetAll(parameters);
				if (droits is null)
				{
					_logger.LogWarning("Détails d'un droit : Opération effectuée avec succès");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun menu trouvé"));
				}
				var result = _mapper.Map<IEnumerable<HabilitationProfilRessource>>(droits);
				var metadata = new
				{
					droits.PageSize,
					droits.CurrentPage,
					droits.TotalCount,
					droits.TotalPages,
					droits.HasNext,
					droits.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation("Liste des droits : Opération effectuée avec succès");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult<HabilitationProfilRessource>> Add(HabilitationProfilRessource ressource)
		{
			try
			{
				//Validation
				var validation = new HabilitationProfilValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Enregistrement d'un droit : champs obligatoires");
					return BadRequest();
				}
				var profil = await _profilService.GetProfilById(ressource.ProfilId);
				if(profil is null)
				{
					_logger.LogWarning("Enregistrement d'un droit : Profil sélectionné introuvable");
					return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: " Profil sélectionné introuvable"));
				}
				var droits = await _habilitationProfilService.GetAll();
				droits = droits.Where(x => x.ProfilId == ressource.ProfilId).ToList();
				if(droits.Count() != 0)
				{
					_logger.LogWarning("Enregistrement d'un droit : Ce droit est déjà attribué au profil sélectionné");
					return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_ALREADY_EXISTS, description: " Ce droit est déjà attribué au profil sélectionné"));
				}
				//Mappage
				var droit = _mapper.Map<HabilitationProfil>(ressource);

				//Enregistrement
				var droitCreated = await _habilitationProfilService.Create(droit);
				

				//Mappage en vue de retourner la ressource à l'utilisateur
				var result = _mapper.Map<HabilitationProfilRessource>(droitCreated);

				_logger.LogInformation("Enregistrement d'un droit : Opération effectuée avec succès");
				return Ok(result);
			}
			catch (Exception ex)
			{
				
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<HabilitationProfilRessource>> GetById(int id)
		{
			try
			{
				var droit = await _habilitationProfilService.GetHabilitationProfilById(id);
				if (droit is null)
				{
					_logger.LogWarning($"'Détails d'un droit :  Ressource inexistante");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource inexistante"));
				}

				var ressource = _mapper.Map<HabilitationProfilRessource>(droit);

				_logger.LogInformation("Détails d'un droit : Opération effectuée avec succès");

				return Ok(ressource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.NOT_FOUND, title: "Ressource non trouvée", detail: ex.Message);
			}
		}

		[HttpPut("Edition")]
		public async Task<ActionResult<ParamMotDePasseRessource>> UpdateEdition(UpdateHabilitationRessource ressource)
		{
			try
			{
				var droit = await _habilitationProfilService.GetHabilitationProfilById(ressource.Id);
				if (droit is null)
				{
					_logger.LogWarning("Mise à jour du droit d'édition : aucun droit existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun droit existant"));
				}
				var paramUpdated = await _habilitationProfilService.UpdateEdition(droit, ressource.Response);
				var paramResult = _mapper.Map<HabilitationProfilRessource>(paramUpdated);

				_logger.LogInformation("Modification du droit d'édition : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("Insertion")]
		public async Task<ActionResult<ParamMotDePasseRessource>> UpdateInsertion(UpdateHabilitationRessource ressource)
		{
			try
			{
				var droit = await _habilitationProfilService.GetHabilitationProfilById(ressource.Id);
				if (droit is null)
				{
					_logger.LogWarning("Mise à jour du droit d'insertion : aucun droit existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun droit existant"));
				}
				var paramUpdated = await _habilitationProfilService.UpdateInsertion(droit, ressource.Response);
				var paramResult = _mapper.Map<HabilitationProfilRessource>(paramUpdated);

				_logger.LogInformation("Modification du droit d'insertion : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("Modification")]
		public async Task<ActionResult<ParamMotDePasseRessource>> UpdateModification(UpdateHabilitationRessource ressource)
		{
			try
			{
				var droit = await _habilitationProfilService.GetHabilitationProfilById(ressource.Id);
				if (droit is null)
				{
					_logger.LogWarning("Mise à jour du droit de modification : aucun droit existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun droit existant"));
				}
				var paramUpdated = await _habilitationProfilService.UpdateModification(droit, ressource.Response);
				var paramResult = _mapper.Map<HabilitationProfilRessource>(paramUpdated);

				_logger.LogInformation("Modification du droit de modification : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}


		[HttpPut("Generation")]
		public async Task<ActionResult<ParamMotDePasseRessource>> UpdateGeneration(UpdateHabilitationRessource ressource)
		{
			try
			{
				var droit = await _habilitationProfilService.GetHabilitationProfilById(ressource.Id);
				if (droit is null)
				{
					_logger.LogWarning("Mise à jour du droit de génération : aucun droit existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun droit existant"));
				}
				var paramUpdated = await _habilitationProfilService.UpdateGeneration(droit, ressource.Response);
				var paramResult = _mapper.Map<HabilitationProfilRessource>(paramUpdated);

				_logger.LogInformation("Modification du droit de modification : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("Suppression")]
		public async Task<ActionResult<ParamMotDePasseRessource>> UpdateSuppression(UpdateHabilitationRessource ressource)
		{
			try
			{
				var droit = await _habilitationProfilService.GetHabilitationProfilById(ressource.Id);
				if (droit is null)
				{
					_logger.LogWarning("Mise à jour du droit de génération : aucun droit existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun droit existant"));
				}
				var paramUpdated = await _habilitationProfilService.UpdateSuppression(droit, ressource.Response);
				var paramResult = _mapper.Map<HabilitationProfilRessource>(paramUpdated);

				_logger.LogInformation("Modification du droit de modification : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				var droit = await _habilitationProfilService.GetHabilitationProfilById(id);
				if (droit is null)
				{
					_logger.LogWarning("Suppression d'un droit : aucun droit existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun droit existant"));
				}
				await _habilitationProfilService.Delete(droit);
				_logger.LogInformation("Suppression d'un droit : Opération effectuée avec succès");

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
