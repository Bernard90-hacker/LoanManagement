namespace LoanManagement.API.Controllers.Loan_Management
{
	[ApiController]
	[Route("api/Loan/[controller]")]
	public class EmployeurController : Controller
	{
		private readonly IEmployeurService _employeurService;
		private readonly IDossierClientService _dossierClientService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly JournalisationService _journalisationService;
		private readonly IConfiguration _configuration;

        public EmployeurController(IEmployeurService employeurService, IConfiguration config,
			ILoggerManager logger, IMapper mapper, JournalisationService journalisationService,
			IDossierClientService dossierClientService)
        {
			_employeurService = employeurService;
			_logger = logger;
			_mapper = mapper;
			_journalisationService = journalisationService;
			_configuration = config;
			_dossierClientService = dossierClientService;
        }

		[HttpGet("all")]
		public async Task<ActionResult> GetAll(EmployeurParameters parameters)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Liste des employeurs", TypeJournalId = 8, Entite = "User" };
					try
					{
						var all = await _employeurService.GetAll(parameters);
						var result = _mapper.Map<IEnumerable<EmployeurRessource>>(all);
						var metadata = new
						{
							all.PageSize,
							all.TotalCount,
							all.TotalPages,
							all.CurrentPage
						};
						Journal.Niveau = 2; //SUCCES;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Liste des employeurs : Opération effectuée avec succès");
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

		[HttpPost("add")]
		public async Task<ActionResult> Add(EmployeurRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Enregistrement d'un employeur", TypeJournalId = 5, Entite = "User" };
					try
					{
						var validation = new EmployeurRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Enregistrement d'un employeur : Champs obligatoires");
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, description : "Champs obligatoires"));
						}
						var employeur = _mapper.Map<Employeur>(ressource);
						var employeurCreated = await _employeurService.Create(employeur);
						var result = _mapper.Map<EmployeurRessource>(employeurCreated);
						Journal.Niveau = 2; //SUCCES
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Enregistrement d'un employeur : Opération effectuée avec succès");


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

		[HttpGet("id")]
		public async Task<ActionResult> GetById(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var journal = new Journal() { Libelle = "Récupération d'un employeur par son id", Entite = "User", TypeJournalId = 6 };
					try
					{
						var employeur = await _employeurService.GetById(id);
						if (employeur is null)
						{
							_logger.LogWarning("Détails d'un employeur : employeur introuvable");
							journal.Niveau = 2;
							await _journalisationService.Journalize(journal);
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "employeur introuvable"));
						}
						journal.Niveau = 3;
						await _journalisationService.Journalize(journal);
						_logger.LogInformation("Liste des employeurs : Opération effectuée avec succès");
						var result = _mapper.Map<EmployeurRessource>(employeur);
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

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{

			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Suppression d'un employeur", TypeJournalId = 4, Entite = "User" };
					try
					{
						var employeur = await _employeurService.GetById(id);
						if(employeur is null)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Détails d'un employeur : Ressource inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description:
								"Ressource inexistante"));
						}
						var dossier = await _employeurService.GetDossier(id);
						if(dossier is not null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Suppression d'un employeur : Impossible d'effectuer cette opération, " +
								"cet employeur est lié à un dossier crédit");
							return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Impossible d'effectuer cette opération, cet employeur est lié à un dossier crédit"));
						}

						await _employeurService.Delete(employeur);
						await transaction.CommitAsync();
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
