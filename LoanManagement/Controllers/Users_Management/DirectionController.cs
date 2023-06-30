using Constants.Pagination;
using System.Numerics;

namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]/")]
	public class DirectionController : Controller
	{
		private readonly IDirectionService _directionService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly IConfiguration _config;

        public DirectionController(IDirectionService directionService,
			ILoggerManager logger, IConfiguration config, IMapper mapper)
        {
			_directionService = directionService;
			_logger = logger;
			_config = config;
			_mapper = mapper;
        }

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<DirectionRessource>>> GetAllDirections([FromQuery] DirectionParameters parameters)
		{
			try
			{
				var directions = await _directionService.GetAll(parameters);
				if(directions is null)
				{
					_logger.LogWarning("'Détails d'une direction' : La direction n'a pas été trouvée");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Direction(s) non trouvée(s)"));
				}
				var directionResults = _mapper.Map<IEnumerable<DirectionRessource>>(directions);
				var metadata = new
				{
					directions.PageSize,
					directions.CurrentPage,
					directions.TotalCount,
					directions.TotalPages,
					directions.HasNext,
					directions.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation($"'Liste des directions ': Opération effectuée avec succès, {directions.Count} comptes retournés");
				return Ok(directionResults);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult<IEnumerable<DirectionRessource>>> Add([FromQuery] DirectionRessource ressource)
		{
			try
			{
				//Validation
				var validation = new SaveDirectionValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Enregistrement d'une direction : champs obligatoires");
					return BadRequest();
				}
				//Mappage
				var direction = _mapper.Map<Direction>(ressource);

				//Enregistrement
				var directionCreated = await _directionService.Create(direction);

				//Mappage en vue de retourner la ressource à l'utilisateur
				var directionResult = _mapper.Map<DirectionRessource>(directionCreated);

				_logger.LogInformation("Enregistrement d'une direction : Opération effectuée avec succès");
				return Ok(directionResult);
			}
			catch (Exception ex)
			{
				
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{code}")]
		public async Task<ActionResult<DirectionRessource>> GetDirectionByCode(string code)
		{
			try
			{
				var direction = await _directionService.GetDirectionByCode(code);
				if (direction is null)
				{
					_logger.LogWarning($"'Détails d'une direction :  Direction inexistante");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Direction inexistante"));
				}

				var directionRessource = _mapper.Map<DirectionRessource>(direction);

				_logger.LogInformation("Opération effectuée avec succès");

				return Ok(directionRessource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.NOT_FOUND, title: "Ressource non trouvée", detail: ex.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<DirectionRessource>> GetDirectionById(int id)
		{
			try
			{
				var direction = await _directionService.GetDirectionById(id);
				if (direction is null)
				{
					_logger.LogWarning($"'Détails d'une direction :  Direction inexistante");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Direction inexistante"));
				}

				var directionRessource = _mapper.Map<DirectionRessource>(direction);

				_logger.LogInformation("Opération effectuée avec succès");

				return Ok(directionRessource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.NOT_FOUND, title: "Ressource non trouvée", detail: ex.Message);
			}
		}

		[HttpGet("{code}/departements")]
		public async Task<ActionResult<PagedList<DepartementRessource>>> GetAllDepartmentsByDirection(string code)
		{
			try
			{
				var departements = await _directionService.GetAllDepartementsByDirection(code);
				if (departements is null)
				{
					_logger.LogWarning("'Détails d'un département' : La direction n'a pas été trouvée");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Département(s) non trouvé(s)"));
				}
				var departementResults = _mapper.Map<IEnumerable<DepartementRessource>>(departements);
				var metadata = new
				{	
					departements.PageSize,
					departements.CurrentPage,
					departements.TotalCount,
					departements.TotalPages,
					departements.HasNext,
					departements.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation($"'Liste des départements de la direction {code} ': Opération effectuée avec succès, " +
					$"{departements.Count} départements retournés");
				return Ok(departementResults);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpDelete("{code}")]
		public async Task<ActionResult> Delete(string code)
		{
			try
			{
				//Validation
				var direction = await _directionService.GetDirectionByCode(code);
				if (direction is null)
				{
					_logger.LogWarning("Mise à jour d'une direction : Direction non trouvée");
					return BadRequest();
				}
				var depts = await _directionService.GetAllDepartementsByDirection(code);
				if(depts.Count() != 0)
				{
					_logger.LogWarning("Suppression d'une direction : Il existe des départements associés à cette direction");
					return BadRequest("Impossible d'exécuter cette requête, veuillez supprimer les départements d'abord");
				}
				//Suppression
				await _directionService.Delete(direction);
				
				_logger.LogInformation("Suppression d'une direction : Opération effectuée avec succès");
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("update")]
		public async Task<ActionResult<DirectionRessource>> Update(DirectionRessource ressource)
		{
			try
			{
				//Validation
				var direction = await _directionService.GetDirectionByCode(ressource.Code);
				var validation = new SaveDirectionValidator();
				var validationResult = await validation.ValidateAsync(ressource);

				if (direction is null)
				{
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Cette direction n'existe pas"));
				}

				if (!validationResult.IsValid)
				{
					_logger.LogWarning($"'Mise à jour de la direction {ressource.Code} :  champs non conformes");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Champs de validation non conformes"));
				}
				//Mise à jour
				var directionUpdated = await _directionService.Update(direction, ressource.Libelle);
				//Mappage
				var directionRessource = _mapper.Map<Direction, DirectionRessource>(directionUpdated);


				_logger.LogInformation("Mise à jour des informations d'une direction : Opération effectuée avec succès");
				return Ok(directionRessource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}



	}
}
