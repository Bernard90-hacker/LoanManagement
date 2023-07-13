using LoanManagement.API.Dto;
using LoanManagement.core.Models.Loan_Management;
using LoanManagement.core.Models.Users_Management;

namespace LoanManagement.API.Controllers.Loan_Management
{
	[ApiController]
	[Route("api/Loan/[controller]")]
	public class CompteController : Controller
	{
		private readonly ICompteService _compteService;
		private readonly IClientService _clientService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly IJournalService _journalService;
		private readonly IUtilisateurService _utilisateurService;
		private readonly ITypeJournalService _typeJournalService;
		private readonly JournalisationService _journalisationService;
		private readonly IConfiguration _configuration;

		public CompteController(ICompteService compteService, IMapper mapper, ILoggerManager logger,
			IJournalService journalService, IUtilisateurService utilisateurService,
			ITypeJournalService typeJournalService, IClientService clientService,
			JournalisationService journalisationService, IConfiguration config)
		{
			_compteService = compteService;
			_mapper = mapper;
			_clientService = clientService;
			_logger = logger;
			_journalService = journalService;
			_utilisateurService = utilisateurService;
			_typeJournalService = typeJournalService;
			_journalisationService = journalisationService;
			_configuration = config;
		}

		[HttpGet("all")]
		public async Task<ActionResult> GetAll([FromQuery] CompteParameters parameters)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var journal = new Journal() { Libelle = "Liste des comptes", TypeJournalId = 8, Entite = "User" };
					try
					{
						var result = await _compteService.GetAll(parameters);
						var metadata = new
						{
							result.PageSize,
							result.CurrentPage,
							result.TotalCount,
							result.TotalPages,
							result.HasNext,
							result.HasPrevious
						};
						var finalResult = _mapper.Map<IEnumerable<GetCompteDto>>(result);
						journal.Niveau = 3;
						await _journalisationService.Journalize(journal);
						Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
						_logger.LogInformation($"'Liste des modules ': Opération effectuée avec succès, {result.Count} modules retournés");

						return Ok(finalResult);
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
		public async Task<ActionResult> Add(CompteRessource ressource)
		{
			using(var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using(var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Création d'un compte courant", TypeJournalId = 5, Entite = "User" };
					try
					{
						var validation = new CompteRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Création d'un compte : Champs obligatoires");
							return BadRequest(new ApiResponse((int)CustomHttpCode.PROCESS_ERRORS, description: "Champs obligatoires"));
						}
						var compte = await _compteService.GetByNumber(ressource.NumeroCompte);
						if(compte is not null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Création d'un compte : Un compte avec le même numéro existe déjà");
							return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_ALREADY_EXISTS, description: "Un compte avec le même numéro existe déjà"));
						}
						var client = await _clientService.GetById(ressource.ClientId);
						if(client is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Création d'un compte : Le client renseigné n'existe pas");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_ALREADY_EXISTS, description: "Le client renseigné n'existe pas"));
						}
						var compteDb = _mapper.Map<Compte>(ressource);
						var compteCreated = await _compteService.Create(compteDb);
						var result = _mapper.Map<CompteRessource>(compteCreated);
						_logger.LogInformation("Création d'un compte : Opération effectuée avec succès");
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
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

		[HttpPut("debit")]
		public async Task<ActionResult> Debit(UpdateCompteRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = $"Débiter le compte  {ressource.NumeroCompte}", Entite = "User", TypeJournalId = 3 };
					try
					{
						var compte = await _compteService.GetByNumber(ressource.NumeroCompte);
						if (compte is null)
						{
							Journal.Niveau = 2;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning($"Débit d'un compte : Compte non trouvé");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Compte non trouvé"));
						}
						await _compteService.IncreaseAmount(compte, ressource.Solde);
						_logger.LogInformation($"Débit du compte : Opération effectuée avec succès");
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
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

		[HttpPut("credit")]
		public async Task<ActionResult> Credit(UpdateCompteRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = $"Débiter le compte  {ressource.NumeroCompte}", Entite = "User", TypeJournalId = 3 };
					try
					{
						var compte = await _compteService.GetByNumber(ressource.NumeroCompte);
						if (compte is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning($"Crédit d'un compte : Compte non trouvé");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Compte non trouvé"));
						}
						if(compte.Solde < ressource.Solde)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning($"Crédit d'un compte : Solde insuffisant");
							return NotFound(new ApiResponse((int)CustomHttpCode.WARNING, description: "Solde insuffisant"));
						}
						await _compteService.DecreaseAmount(compte, ressource.Solde);
						_logger.LogInformation($"Crédit d'un compte : Opération effectuée avec succès");
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
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
