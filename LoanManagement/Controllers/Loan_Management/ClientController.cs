using LoanManagement.core.Models.Loan_Management;
using LoanManagement.core.Models.Users_Management;

namespace LoanManagement.API.Controllers.Loan_Management
{
	[ApiController]
	[Route("api/Loan/[controller]")]
	public class ClientController : Controller
	{
		private readonly IClientService _clientService;
		private readonly IDossierClientService _dossierClientService;
		private readonly IPretAccordService _pretAccordService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly IJournalService _journalService;
		private readonly IUtilisateurService _utilisateurService;
		private readonly ITypeJournalService _typeJournalService;
		private readonly JournalisationService _journalisationService;
		private readonly IConfiguration _configuration;

		public ClientController(IClientService clientService, IMapper mapper, ILoggerManager logger,
			IJournalService journalService, IUtilisateurService utilisateurService,
			ITypeJournalService typeJournalService, IDossierClientService dossierClientService,
			IPretAccordService pretAccordService,
			JournalisationService journalisationService, IConfiguration config)
        {
			_clientService = clientService;
			_mapper = mapper;
			_logger = logger;
			_journalService = journalService;
			_utilisateurService = utilisateurService;
			_typeJournalService = typeJournalService;
			_journalisationService = journalisationService;
			_dossierClientService = dossierClientService;
			_pretAccordService = pretAccordService;
			_configuration = config;
        }

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<ClientRessource>>> GetAll()
		{
			using(var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using(var transaction = connection.BeginTransaction())
				{
					var journal = new Journal() { Libelle = "Liste des clients", TypeJournalId = 8, Entite = "User" };
					try
					{
						var result = await _clientService.GetAll();
						var finalResult = _mapper.Map<IEnumerable<ClientRessource>>(result);
						journal.Niveau = 3; //INFO
						await _journalisationService.Journalize(journal);
						_logger.LogInformation($"'Liste des modules ': Opération effectuée avec succès, {result.Count()} modules retournés");

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

		[HttpGet("{id}/prets")]
		public async Task<ActionResult> GetLoan(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Liste des demandes de prêt d'un client", TypeJournalId = 8, Entite = "User" };
					try
					{
						var client = await _clientService.GetById(id);
						if(client is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Liste des demandes de prêts d'un client : Client sélectionné introuvable");

							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Client sélectionné introuvable"));
						}
						var dossiers = await _clientService.GetDossierClient(id);
						Journal.Niveau = 3; //INFORMATION
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();

						return Ok(dossiers);
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

		[HttpPost("login")]
		public async Task<ActionResult> Login(ClientLoginRessource ressource)
		{
			using (	SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (SqlTransaction transaction = connection.BeginTransaction())
				{
					var journal = new Journal() { Libelle = $"Connexion d'un client", 
						TypeJournalId = 1, 
						Entite = "Client" };
					try
					{
						var validation = new ClientLoginValidator();
						var validationResult = await validation.ValidateAsync(ressource);

						if (!validationResult.IsValid)
						{
							_logger.LogWarning("Connexion d'un client : Champs obligatoires");
							journal.Niveau = 1;
							await _journalisationService.Journalize(journal);
							transaction.Commit();
							return BadRequest();
						}
						var client = await _clientService.GetByIndice(ressource.Indice);
						if (client is null)
						{
							_logger.LogWarning("Connexion d'un client : Indice incorrect");
							journal.Niveau = 1;
							await _journalisationService.Journalize(journal);
							transaction.Commit();
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Indice incorrect"));
						}
						if (client.Tel != ressource.Telephone)
						{
							//journal.Niveau = 1;
							//await _journalisationService.Journalize(journal);
							//transaction.Commit();
							_logger.LogWarning("Connexion d'un client : Numéro de téléphone incorrect");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, "Numéro de téléphone incorrect"));
						}
						var result = _mapper.Map<ClientRessource>(client);
						//journal.Niveau = 2;
						//await _journalisationService.Journalize(journal);
						_logger.LogInformation("Connexion d'un client : Opération effectuée avec succès");
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


		[HttpGet("id")]
		public async Task<ActionResult> GetById(int id)
			{
			using(var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var journal = new Journal() { Libelle = "Récupération d'un client par son id", Entite = "User", TypeJournalId = 6 };
					try
					{
						var customer = await _clientService.GetById(id);
						if(customer is null)
						{
							_logger.LogWarning("Détails d'un client : Client introuvable");
							journal.Niveau = 2;
							await _journalisationService.Journalize(journal);
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Client introuvable"));
						}
						journal.Niveau = 3;
						await _journalisationService.Journalize(journal);
						_logger.LogInformation("Liste des clients : Opération effectuée avec succès");
						var result = _mapper.Map<ClientRessource>(customer);
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
		public async Task<ActionResult> Add(ClientRessource ressource)
		{
			using(var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using(var transaction = connection.BeginTransaction())
				{
					var journal = new Journal() { Libelle = "Enregistrement d'un client", Entite = "Utilisateur", TypeJournalId = 5 };
					try
					{
						var validation = new ClientRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							journal.Niveau = 1;
							await _journalisationService.Journalize(journal);
							_logger.LogWarning("Enregistrement d'un client : Champs obligatoires");
							return BadRequest();
						}
						var customerFullName = $"{ressource.Nom} {ressource.Prenoms}";
						var customer = await _clientService.GetCustomerByFullName(customerFullName);
						if(customer is not null)
						{
							journal.Niveau = 1;
							await _journalisationService.Journalize(journal);
							_logger.LogWarning("Enregistrement d'un client : Un client avec le même nom et prénoms existe déjà");
							return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_ALREADY_EXISTS, description: "Un client avec le même nom et prénoms existe déjà"));
						}
						var customerIndice = await _clientService.GetByIndice(ressource.Indice);
						var customerPhone = await _clientService.GetByPhoneNumber(ressource.Tel);
						if(customerIndice is not null)
						{
							journal.Niveau = 1;
							await _journalisationService.Journalize(journal);
							_logger.LogWarning("Enregistrement d'un client : Un client avec le même indice existe déjà");
							return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_ALREADY_EXISTS, description: "Un client avec le même indice existe déjà"));
						}
						if (customerPhone is not null)
						{
							journal.Niveau = 1;
							await _journalisationService.Journalize(journal);
							_logger.LogWarning("Enregistrement d'un client : Un client avec le même numéro de téléphone existe déjà");
							return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_ALREADY_EXISTS, description: "Un client avec le même numéro de téléphone existe déjà"));
						}
						var customerDb = _mapper.Map<Client>(ressource);
						var customerCreated = await _clientService.Create(customerDb);
						var result = _mapper.Map<ClientRessource>(customerCreated);
						_logger.LogInformation("Enregistrement d'un client : Opération effectuée avec succès");
						journal.Niveau = 2;
						await _journalisationService.Journalize(journal);
						return Ok(result);
					}
					catch (Exception ex)
					{
						await transaction.RollbackAsync();
						journal.Niveau = 1;
						await _journalisationService.Journalize(journal);
						_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
						return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
					}
				}
			}
		}

		[HttpGet("{id}/comptes")]
		public async Task<ActionResult> GetComptes(int id)
		{
			using(var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using(var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = $"Liste des comptes du client {id}", Entite = "User", TypeJournalId = 8 };

					try
					{
						var customer = await _clientService.GetById(id);
						if(customer is null)
						{
							_logger.LogWarning("Détails d'un client : Client introuvable");
							Journal.Niveau = 2;
							await _journalisationService.Journalize(Journal);
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Client introuvable"));
						}

						var comptes = await _clientService.GetCompte(id);
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						_logger.LogInformation("Liste des comptes du client n° {id} : Opération effectuée avec succès");
						var result = _mapper.Map<GetCompteDto>(comptes);

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
