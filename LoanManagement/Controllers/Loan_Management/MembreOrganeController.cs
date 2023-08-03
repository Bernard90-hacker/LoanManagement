namespace LoanManagement.API.Controllers.Loan_Management
{
	[ApiController]
	[Route("api/Loan/[controller]")]
	public class MembreOrganeController : Controller
	{
		private readonly IMembreOrganeService _membreService;
		private readonly IOrganeDecisionService _organeDecisionService;
		private readonly IUtilisateurService _utilisateurService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly ITypeJournalService _typeJournalService;
		private readonly JournalisationService _journalisationService;
		private readonly IConfiguration _configuration;

		public MembreOrganeController(IMembreOrganeService membreService, IMapper mapper, 
			ILoggerManager logger, ITypeJournalService typeJournalService,
			JournalisationService journalisationService, IConfiguration config,
			IUtilisateurService utilisateurService, IOrganeDecisionService organeDecisionService)
		{
			_membreService = membreService;
			_mapper = mapper;
			_logger = logger;
			_typeJournalService = typeJournalService;
			_journalisationService = journalisationService;
			_configuration = config;
			_utilisateurService = utilisateurService;
			_organeDecisionService = organeDecisionService;
		}

		[HttpGet("all")]
		public async Task<ActionResult> GetAll([FromQuery] MembreOrganeParameters parameters)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Liste des membres intervenant dans le processus de traitement d'un crédit", 
						TypeJournalId = 8, Entite = "User" };
					try
					{
						var all = await _membreService.GetAll(parameters);
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						_logger.LogInformation("Liste des membres intervenant dans le processus de traitement d'un crédit");
						var result = _mapper.Map<IEnumerable<MembreOrganeRessource>>(all);
						var metadata = new
						{
							all.PageSize,
							all.CurrentPage,
							all.TotalCount,
							all.TotalPages,
							all.HasNext,
							all.HasPrevious
						};
						Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
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

		[HttpGet("{id}/username")]
		public async Task<ActionResult> GetMembreUsername(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Récupération du nom d'utilisateur d'un membre faisant partie du circuit de décision", TypeJournalId = 8, Entite = "User" };
					try
					{
						var membre = await _membreService.GetById(id);
						if(membre is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Détails d'un membre : Ressource inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource inexistante"));
						}
						var result = await _membreService.GetMembreUsername(membre.Id);
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						_logger.LogInformation($"Nom d'utilisateur du membre n° {id} : Opération effectuée avec succès");

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

		[HttpGet("{id}/etape")]
		public async Task<ActionResult> GetEtape(int id)// L'utilisateur connecté est à quelle étape du processus de décision
		{
			using(var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using(var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Identification de l'étape à laquelle intervient un utilisateur dans le processus de décision", TypeJournalId = 8, Entite = "User" };
					try
					{
						var user = await _utilisateurService.GetUserById(id);
						if(user is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Identification de l'étape à laquelle intervient un utilisateur dans le processus de décision : Utilisateur inexistant");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur inexistant"));
						}
						var result = await _membreService.GetEtapeByUser(user.Id);
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						_logger.LogInformation("Identification de l'étape à laquelle intervient un utilisateur dans le processus de décision : Opération effectuée avec succès");

						return Ok(result);
					}
					catch (Exception ex)
					{
                        _logger.LogError("Une erreur est survenue pendant le traitement de la requête");
                        return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
                    }	
				}
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult> Add(MembreOrganeRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Enregistrement d'un membre", TypeJournalId = 5, Entite = "User" };
					try
					{
						var user = _utilisateurService.GetUserById(ressource.UtilisateurId);
						if(user is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Enregistrement d'un membre : Utilisateur sélectionné inexistant");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur sélectionné inexistant"));
						}
						var organe = _organeDecisionService.GetById(ressource.OrganeDecisionId);
						if(organe is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Enregistrement d'un membre : Organe de décision sélectionné inexistant");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Organe de décision sélectionné inexistant"));
						}
						var membre = _mapper.Map<MembreOrgane>(ressource);
						var membreCreated = await _membreService.Create(membre);
						var result = _mapper.Map<MembreOrganeRessource>(membreCreated);
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();

						_logger.LogInformation("Enregistrement d'un membre : Opération effectuée avec succès");
						return Ok(result);
					}
					catch (Exception ex)
					{
						await transaction.RollbackAsync();
						Journal.Niveau = 2;
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
					var Journal = new Journal() { Libelle = "Identification d'un membre par son id", TypeJournalId = 6, Entite = "User" };
					try
					{
						var membre = await _membreService.GetById(id);
						if(membre is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Détails d'un membre : Ressource inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Ressource inexistante"));
						}
						var result = _mapper.Map<MembreOrganeRessource>(membre);
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();

						_logger.LogInformation("Détails d'un membre : Opération effectuée avec succès");
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

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{

			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Suppression d'un membre organe", 
						TypeJournalId = 4, Entite = "User" };
					try
					{
						var membre = await _membreService.GetById(id);
						if (membre is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Détails d'un membre : Ressource inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource inexistante"));
						}
						await _membreService.Delete(membre);
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();

						_logger.LogInformation("Suppression d'un membre : Opération effectuée avec succès");
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
