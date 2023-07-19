namespace LoanManagement.API.Controllers.Loan_Management
{
	[ApiController]
	[Route("api/Loan/[controller]")]
	public class DossierClientController : Controller
	{
		private readonly IDossierClientService _dossierClientService;
		private readonly INatureQuestionService _natureQuestionService;
		private readonly IClientService _clientService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly JournalisationService _journalisationService;
		private readonly IConfiguration _configuration;
		private readonly ISanteClientService _infoSanteClientService;
		private readonly IStatutDossierClientService _statutDossierClientService;
		private readonly IStatutMaritalService _statutMaritalService;
		public DossierClientController(IDossierClientService dossierClientService, IMapper mapper,
			ILoggerManager logger, JournalisationService journalisationService, 
			IConfiguration configuration, ISanteClientService infoSanteClientService,
			IStatutDossierClientService statutDossierClientService, IStatutMaritalService statutMaritalService,
			IClientService clientService, INatureQuestionService natureQuestionService)
        {
			_dossierClientService = dossierClientService;
			_mapper = mapper;
			_logger = logger;
			_configuration = configuration;
			_journalisationService = journalisationService;
			_infoSanteClientService = infoSanteClientService;
			_statutDossierClientService = statutDossierClientService;
			_statutMaritalService = statutMaritalService;
			_clientService = clientService;
			_natureQuestionService = natureQuestionService;
        }

		[HttpGet("all")]
		public async Task<ActionResult> GetAll()
		{

			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Liste des demandes de prêts", TypeJournalId = 8,
						Entite = "User" };
					try
					{
						var all = await _dossierClientService.GetAll();
						var result = _mapper.Map<IEnumerable<DossierClientRessource>>(all);
						Journal.Niveau = 3; //INFORMATION
						await _journalisationService.Journalize(Journal);
						_logger.LogInformation("Liste des demandes de prêts : Opération effectuée avec succès");

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

		[HttpGet("{id}")]
		public async Task<ActionResult> GetById(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var journal = new Journal() { Libelle = "Identification d'un dossier crédit par son id", Entite = "User", TypeJournalId = 6 };
					try
					{
						var dossier = await _dossierClientService.GetById(id);
						if (dossier is null)
						{
							_logger.LogWarning("Détails d'un dossier crédit : Ressource introuvable");
							journal.Niveau = 1; // ECHEC
							await _journalisationService.Journalize(journal);
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource introuvable"));
						}
						journal.Niveau = 3;
						await _journalisationService.Journalize(journal);
						_logger.LogInformation("Détails d'un dossier crédit : Opération effectuée avec succès");
						var result = _mapper.Map<DossierClientRessource>(dossier);

						return Ok(result);
					}
					catch (Exception ex)
					{
						journal.Niveau = 1;
						await _journalisationService.Journalize(journal);
						_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
						return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
					}
				}
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult> Add(DossierClientRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Enregistrement d'une demande de prêt", 
						TypeJournalId = 5, Entite = "Client" };
					try
					{
						var statutMarital = await _statutMaritalService.GetById(ressource.StatutMaritalId);
						var client = await _clientService.GetById(ressource.ClientId);
						if(statutMarital is null || client is null)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Enregistrement d'une demande de prêt : Statut marital ou client sélectionné invalide(s)");

							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Statut marital ou client sélectionné invalide (s)"));
						}
						var dossier = _mapper.Map<DossierClient>(ressource);
						dossier.ClientId = ressource.ClientId;
						var dossierCreated = await _dossierClientService.Create(dossier);
						var dossierCreatedSuccessFully = _mapper.Map<DossierClientRessource>(dossierCreated);
						_logger.LogInformation("Enregistrement d'une demande de prêt : Opération effectuée avec succès");

						return Ok(dossierCreatedSuccessFully);
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
		[HttpPost("addInfoSanteClient")]
		public async Task<ActionResult> Add(InfoSanteClientRessource ressource)
		{

			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Enregistrement des informations de santé", 
						TypeJournalId = 5, Entite = "Client" };
					try
					{
						var validation = new InfoSanteClientRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();

							_logger.LogWarning("Enregistrement des informations liées à la santé du client : Champs obligatoires");
							return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Champs obligatoires"));
						}
						var dossierClient = await _dossierClientService.GetById(ressource.DossierClientId);
						var question = await _natureQuestionService.GetById(ressource.NatureQuestionId);
						if(dossierClient is null || question is null)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Enregistrement des informations liées à la santé du client : Statut marital ou client sélectionné invalide(s)");

							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Statut marital ou client sélectionné invalide (s)"));
						}
						var infoSante = _mapper.Map<InfoSanteClient>(ressource);
						var infoSanteCreated = await _infoSanteClientService.Create(infoSante);
						var result = _mapper.Map<InfoSanteClientRessource>(infoSante);
						Journal.Niveau = 2; //SUCCES
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Enregistrement des informations liées à la santé du client : Opération effectuée avec succès");

						return Ok(result);
					}
					catch (Exception ex)
					{
						await transaction.RollbackAsync();
						Journal.Niveau = 1; //ECHEC
						await _journalisationService.Journalize(Journal);
						_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
						return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
					}
				}
			}
		}

    }
}
