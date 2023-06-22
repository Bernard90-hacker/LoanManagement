namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class DepartementController : Controller
	{
		private readonly IDepartmentService _departementService;
		private readonly IDirectionService _directionService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly IConfiguration _config;

		public DepartementController(IDepartmentService departmentService,
			ILoggerManager logger, IConfiguration config, IMapper mapper, IDirectionService directionService)
		{
			_departementService = departmentService;
			_logger = logger;
			_config = config;
			_mapper = mapper;
			_directionService = directionService;
		}

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<DepartementRessource>>> GetAll([FromQuery] DepartmentParameters parameters)
		{
			try
			{
				var departements = await _departementService.GetAll(parameters);
				if (departements is null)
				{
					_logger.LogWarning("'Détails d'un département' : La direction n'a pas été trouvée");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Departement(s) non trouvée(s)"));
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
				_logger.LogInformation($"'Liste des départements ': Opération effectuée avec succès, {departements.Count} départements retournés");
				return Ok(departementResults);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}


		[HttpPost("add")]
		public async Task<ActionResult<DepartementRessource>> Add(DepartementRessource ressource)
		{
			try
			{
				//Validation
				var validation = new SaveDepartementValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				var directionAssociatedToDepartement = await _directionService.GetDirectionByCode(ressource.DirectionCode);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Enregistrement d'un département : champs obligatoires");
					return BadRequest();
				}
				if (directionAssociatedToDepartement is null)
				{
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucune direction trouvée"));
				}
				//Mappage
				var departement = _mapper.Map<Departement>(ressource);
				departement.DirectionId = (await _directionService.GetDirectionByCode(ressource.DirectionCode)).Id;
				//Enregistrement
				var departementCreated = await _departementService.Create(departement);

				//Mappage en vue de retourner la ressource à l'utilisateur
				var departementResult = _mapper.Map<DepartementRessource>(departementCreated);

				_logger.LogInformation("Enregistrement d'un département : Opération effectuée avec succès");
				return Ok(departementResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<DepartementRessource>> GetDepartmentById(int id)
		{
			try
			{
				var departement = await _departementService.GetDepartmentById(id);
				if (departement is null)
				{
					_logger.LogWarning($"'Détails d'un département :  Département inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Département inexistant"));
				}

				var departementRessource = _mapper.Map<DepartementRessource>(departement);

				_logger.LogInformation("Opération effectuée avec succès");

				return Ok(departementRessource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.NOT_FOUND, title: "Ressource non trouvée", detail: ex.Message);
			}
		}

		[HttpGet("{code}")]
		public async Task<ActionResult<DepartementRessource>> GetDepartmentByCode(string code)
		{
			try
			{
				var departement = await _departementService.GetDepartmentByCode(code);
				if (departement is null)
				{
					_logger.LogWarning($"'Détails d'un département :  Département inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Département inexistant"));
				}

				var departementRessource = _mapper.Map<DepartementRessource>(departement);
				departementRessource.DirectionCode = (await _directionService.GetDirectionById(departement.DirectionId)).Code;

				_logger.LogInformation("Opération effectuée avec succès");

				return Ok(departementRessource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.NOT_FOUND, title: "Ressource non trouvée", detail: ex.Message);
			}
		}

		[HttpDelete("delete")]
		public async Task<ActionResult<DirectionRessource>> Delete([FromQuery] string code)
		{
			try
			{
				//Validation
				var departement = await _departementService.GetDepartmentByCode(code);
				if (departement is null)
				{
					_logger.LogWarning("Mise à jour d'un departement : Département non trouvé");
					return BadRequest();
				}

				//Suppression
				var departementRemoved = _departementService.Delete(departement);


				_logger.LogInformation("Suppression d'un département : Opération effectuée avec succès");
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("update")]
		public async Task<ActionResult<DirectionRessource>> Update([FromQuery] string code, DepartementRessource ressource)
		{
			try
			{
				//Validation
				var departement = await _departementService.GetDepartmentByCode(code);
				var validation = new SaveDepartementValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				var directionAssociatedToDepartement = await _directionService.GetDirectionByCode(ressource.DirectionCode);

				if (departement is null)
				{
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ce département n'existe pas"));
				}

				if (!validationResult.IsValid)
				{
					_logger.LogWarning($"'Mise à jour du département {code} :  champs non conformes");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Champs de validation non conformes"));
				}

				if (directionAssociatedToDepartement is null)
				{
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucune direction trouvée"));
				}

				var newRessource = new DepartementRessource()
				{
					Code = departement.Code,
					Libelle = ressource.Libelle
				};

				//Mappage
				var departementToBeUpdated = _mapper.Map<Departement>(newRessource);
				//Mise à jour
				var departementUpdated = await _departementService.Update(departement, departementToBeUpdated);
				//Mappage
				var departementRessource = _mapper.Map<DepartementRessource>(departementUpdated);


				_logger.LogInformation("Mise à jour des informations d'un département : Opération effectuée avec succès");
				return Ok(departementRessource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}

		}
	}
}
