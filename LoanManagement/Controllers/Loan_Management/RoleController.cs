namespace LoanManagement.API.Controllers.Loan_Management
{
	[ApiController]
	[Route("api/Loan/[controller]")]
	public class RoleController : Controller
	{
		private readonly IRoleOrganeService _roleService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly ITypeJournalService _typeJournalService;
		private readonly JournalisationService _journalisationService;
		private readonly IConfiguration _configuration;

		public RoleController(IRoleOrganeService roleService, IMapper mapper,
			ILoggerManager logger, JournalisationService journalisationService,
			IConfiguration config, ITypeJournalService typeJournalService)
		{
			_roleService = roleService;
			_mapper = mapper;
			_typeJournalService = typeJournalService;
			_journalisationService = journalisationService;
			_configuration = config;
			_logger = logger;
		}

		[HttpGet("all")]
		public async Task<ActionResult> GetAll()
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Liste des rôles", TypeJournalId = 8, Entite = "User" };
					try
					{
						var roles = await _roleService.GetAll();
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						_logger.LogInformation("Liste des rôles : Opération effectuée avec succès");
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

		[HttpPost("add")]
		public async Task<ActionResult> Add(RoleOrganeRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Enregistrement d'un role", TypeJournalId = 5, Entite = "User" };
					try
					{
						var validation = new RoleOrganeRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Enregistrement d'un role : Champs obligatoires");
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, description : "Champs obligatoires"));
						}

						var roleDb = _mapper.Map<RoleOrgane>(ressource);
						var roleCreated = await _roleService.Create(roleDb);
						var result = _mapper.Map<RoleOrganeRessource>(roleCreated);
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Enregistrement d'un role : Opération effectuée avec succès");
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

		[HttpGet("{id}")]
		public async Task<ActionResult> GetById(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Identification d'un rôle par son id", TypeJournalId = 6, Entite = "User" };
					try
					{
						var role = await _roleService.GetById(id);
						if(role is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Détails d'un rôle : Ressource inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource inexistante"));
						}
						var result = _mapper.Map<RoleOrganeRessource>(role);
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Détails d'un rôle : Opération effectuée avec succès");

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

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(UpdateRoleOrganeRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Modification d'un rôle", TypeJournalId = 3, Entite = "User" };
					try
					{
						var role = await _roleService.GetById(ressource.Id);
						if (role is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Détails d'un rôle : Ressource inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource inexistante"));
						}
						var roleDb = _mapper.Map<RoleOrgane>(ressource);
						var roleUpdated = _roleService.Update(role, roleDb);
						var result = _mapper.Map<RoleOrganeRessource>(roleUpdated);
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Modification d'un role  : Opération effectuée avec succès");

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

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Suppression d'un role", TypeJournalId = 4, Entite = "User" };
					try
					{
						var role = await _roleService.GetById(id);
						if (role is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Détails d'un rôle : Ressource inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource inexistante"));
						}
						await _roleService.Delete(role);
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Suppression d'un rôle : Opération effectuée avec succès");

						return Ok();
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


	}
}
