namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class TypeJournalController : Controller
	{
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly ITypeJournalService _typeJournalService;

        public TypeJournalController(ITypeJournalService typeJournalService, IMapper mapper, ILoggerManager logger)
        {
			_logger = logger;
			_mapper = mapper;
			_typeJournalService = typeJournalService;
        }

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<TypeJournalRessource>>> GetAll([FromQuery] TypeJournalParameters parameters)
		{
			try
			{
				var types = await _typeJournalService.GetAll(parameters);
				if (types is null)
				{
					_logger.LogWarning("'Détails d'un département' : Le type de journal n'a pas été trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource (s) non trouvée(s)"));
				}
				var typeResults = _mapper.Map<IEnumerable<TypeJournalRessource>>(types);
				var metadata = new
				{
					types.PageSize,
					types.CurrentPage,
					types.TotalCount,
					types.TotalPages,
					types.HasNext,
					types.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation($"'Liste des types journaux ': Opération effectuée avec succès, {types.Count} départements retournés");
				return Ok(typeResults);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult<TypeJournalRessource>> Add(TypeJournalRessource ressource)
		{
			try
			{
				//Validation
				var validation = new SaveTypeJournalValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Enregistrement d'un type de journal : champs obligatoires");
					return BadRequest("Champs obligatoires");
				}
				//Mappage
				var type = _mapper.Map<TypeJournal>(ressource);

				//Enregistrement
				var typeCreated = await _typeJournalService.Create(type);

				//Mappage en vue de retourner la ressource à l'utilisateur
				var result = _mapper.Map<TypeJournalRessource>(typeCreated);

				_logger.LogInformation("Enregistrement d'une direction : Opération effectuée avec succès");
				return Ok(typeCreated);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{code}")]
		public async Task<ActionResult<DirectionRessource>> GetTypeJournalByCode(string code)
		{
			try
			{
				var type = await _typeJournalService.GetTypeJournalByCode(code);
				if (type is null)
				{
					_logger.LogWarning($"'Détails d'un type de journal :  Ressource inexistante");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource inexistante"));
				}

				var ressource = _mapper.Map<TypeJournalRessource>(type);

				_logger.LogInformation("Détails d'un type de journal : Opération effectuée avec succès");

				return Ok(ressource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.NOT_FOUND, title: "Ressource non trouvée", detail: ex.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<DirectionRessource>> GetTypeJournalById(int id)
		{
			try
			{
				var type = await _typeJournalService.GetTypeJournalById(id);
				if (type is null)
				{
					_logger.LogWarning($"'Détails d'un type de journal :  Ressource inexistante");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource inexistante"));
				}

				var ressource = _mapper.Map<TypeJournalRessource>(type);

				_logger.LogInformation("Détails d'un type de journal : Opération effectuée avec succès");

				return Ok(ressource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.NOT_FOUND, title: "Ressource non trouvée", detail: ex.Message);
			}
		}

		[HttpPut("update")]
		public async Task<ActionResult<TypeJournalRessource>> Update(TypeJournalRessource ressource)
		{
			try
			{
				var typeDb = await _typeJournalService.GetTypeJournalByCode(ressource.Code);
				if (typeDb is null)
				{
					_logger.LogWarning($"'Mise à jour d'un type de journal :  Ressource inexistante");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource inexistante"));
				}
				var type = new TypeJournal()
				{
					Libelle = ressource.Libelle,
					Code = ressource.Code,
					Statut = ressource.Statut
				};

				var typeUpdated = await _typeJournalService.Update(typeDb, type);
				var result = _mapper.Map<TypeJournalRessource>(typeUpdated);
				_logger.LogInformation("Mise à jour d'un type de journal : Opération effectuée avec succès");

				return Ok(result);

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
				var type = await _typeJournalService.GetTypeJournalByCode(code);
				if (type is null)
				{
					_logger.LogWarning("Suppression d'un type de journal : Ressource non trouvée");
					return BadRequest();
				}
				//Suppression
				await _typeJournalService.Delete(type);


				_logger.LogInformation("Suppression d'un type de journal : Opération effectuée avec succès");
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
