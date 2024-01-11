namespace LoanManagement.API.Controllers.Loan_Management
{
	[Route("api/Loan/[controller]")]
	public class OrganeDecisionController : Controller
	{
		private readonly IOrganeDecisionService _organeDecisionService;
		private readonly IRoleOrganeService _roleOrganeService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly ITypeJournalService _typeJournalService;
		private readonly JournalisationService _journalisationService;
		private readonly IConfiguration _configuration;

        public OrganeDecisionController(IOrganeDecisionService organeDecisionService,
			IRoleOrganeService roleOrganeService, IMapper mapper,
			ILoggerManager logger, ITypeJournalService typeJournalService, 
			JournalisationService journalisationService, IConfiguration configuration)
        {
			_organeDecisionService = organeDecisionService;
			_roleOrganeService = roleOrganeService;
			_mapper = mapper;
			_logger = logger;
			_typeJournalService = typeJournalService;
			_journalisationService = journalisationService;
			_configuration = configuration;
		}


		[HttpGet("all")]
		public async Task<ActionResult> GetAll()
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Liste des organes de décision", TypeJournalId = 8, Entite = "User" };
					try
					{
						var all = await _organeDecisionService.GetAll();
						var result = _mapper.Map<IEnumerable<OrganeDecisionRessource>>(all);
						_logger.LogInformation("Liste des organes de décision : Opération effectuée avec succès");
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						
						return Ok(result);
					}
					catch (Exception ex)
					{
						Journal.Niveau = 1;
						await _journalisationService.Journalize(Journal);
						_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
						return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
					}
				}
			}
		}

		[HttpGet("id")]
		public async Task<ActionResult> GetById(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Identification d'un organe de décision par son id", TypeJournalId = 6, Entite = "User" };
					try
					{
						var organe = await _organeDecisionService.GetById(id);
						if (organe is null)
						{
							_logger.LogInformation("Détails d'un organe de décision : Ressource inexistante");
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Ressource inexistante"));
						}
						var result = _mapper.Map<OrganeDecisionRessource>(organe);
						_logger.LogInformation("Détails d'un organe de décision : Opération effectuée avec succès");
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);

						return Ok(result);
					}
					catch (Exception ex)
					{
						Journal.Niveau = 1;
						await _journalisationService.Journalize(Journal);
						_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
						return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
					}
				}
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult> Add([FromBody] OrganeDecisionRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Enregistrement d'un organe de décision", TypeJournalId = 5, Entite = "User" };
					try
					{
						var validation = new OrganeDecisionRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Enregistrement d'un organe de décision : Champs obligatoires");
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, description: "Champs obligatoires"));
						}
						var organe = _mapper.Map<OrganeDecision>(ressource);
						var organeCreated = await _organeDecisionService.Create(organe);
						var result = _mapper.Map<OrganeDecisionRessource>(ressource);
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Enregistrement d'un organe de décision : Opération effectuée avec succès");

						return Ok(result);
					}
					catch (Exception ex)
					{
						await transaction.RollbackAsync();
						Journal.Niveau = 1;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
						return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
					}
				}
			}
		}

		[HttpPut("update")]
		public async Task<ActionResult> Update(UpdateOrganeDecisionRessource ressource)
		{

			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Mise à jour d'un organe de décision", TypeJournalId = 3, Entite = "User" };
					try
					{
						var validation = new UpdateOrganeDecisionRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogInformation("Mise à jour d'un organe de décision : Champs obligatoires");
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, description: "Champs obligatoires"));
						}
						var organeDecision = await _organeDecisionService.GetById(ressource.Id);
						if(organeDecision is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogInformation("Mise à jour d'un organe de décision : Ressource inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Champs obligatoires"));
						}
						var role = await _roleOrganeService.GetById(ressource.RoleOrganeId);
						if(role is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogInformation("Mise à jour d'un organe de décision : Role inexistant");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Rôle inexistant"));
						}
						var organe = _mapper.Map<OrganeDecision>(ressource);
						var organeUpdated = await _organeDecisionService.Update(organeDecision, organe);
						var result = _mapper.Map<OrganeDecisionRessource>(organeUpdated);
						Journal.Niveau = 2; //Succès
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Mise à jour d'un organe de décision : Opération effectuée avec succès");

						return Ok(result);
					}
					catch (Exception ex)
					{
						await transaction.RollbackAsync();
						Journal.Niveau = 1;
						await _journalisationService.Journalize(Journal);
						_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
						return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
					}
				}
			}
		}

		[HttpGet("membres")]
		public async Task<ActionResult> GetUsersByOrganeDecision(int organeDecisionId)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Liste des membres d'un organe de décision", TypeJournalId = 8, Entite = "User" };
					try
					{
						var membres = await _organeDecisionService.GetAll();
						return Ok();
					}
					catch (Exception)
					{

						return Ok();
					}
				}
			}
		}

		[HttpGet("{id}/roles")]
		public async Task<ActionResult> GetRoles(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Liste des rôles d'un organe", TypeJournalId = 5, Entite = "User" };
					try
					{
						var organe = await _organeDecisionService.GetById(id);
						if(organe is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Liste des rôles d'un organe : Organe introuvable");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Organe introuvable"));
						}
						var roles = await _organeDecisionService.GetRoles(organe.Id);
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Liste des rôles d'un organe de décision : Opération effectuée avec succès");
						var result = _mapper.Map<IEnumerable<RoleOrganeRessource>>(roles);

						return Ok(result);
					}
					catch (Exception ex)
					{
						Journal.Niveau = 1;
						await _journalisationService.Journalize(Journal);
						_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
						return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
					}
				}
			}
		}
    }
}
