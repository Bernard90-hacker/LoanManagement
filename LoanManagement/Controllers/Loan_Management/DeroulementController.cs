using LoanManagement.core.Models.Users_Management;

namespace LoanManagement.API.Controllers.Loan_Management
{
	[ApiController]
	[Route("api/Loan/[controller]")]
	public class DeroulementController : Controller
	{
		private readonly IDeroulementService _deroulementService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly ITypeJournalService _typeJournalService;
		private readonly ITypePretService _typePretService;
		private readonly JournalisationService _journalisationService;
		private readonly IConfiguration _configuration;

		public DeroulementController(IDeroulementService deroulementService, IMapper mapper,
			ILoggerManager logger, JournalisationService journalisationService, 
			IConfiguration config, ITypeJournalService typeJournalService, 
			ITypePretService typePretService)
        {
			_deroulementService = deroulementService;
			_mapper = mapper;
			_typeJournalService = typeJournalService;
			_journalisationService = journalisationService;
			_configuration = config;
			_logger = logger;
			_typePretService = typePretService;
        }

		[HttpGet("all")]
		public async Task<ActionResult> GetAll()
		{
			using(var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using(var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Liste des déroulements de prêts", TypeJournalId = 8, Entite = "User" };
					try
					{
						var deroulements = await _deroulementService.GetAll();
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						_logger.LogInformation("Liste des types de prêts : Opération effectuée avec succès");
						var result = _mapper.Map<IEnumerable<GetDeroulementRessource>>(deroulements);

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
			using(var connection =  new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using(var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Identification d'un type de déroulement par son id", Entite = "User", TypeJournalId = 6 };
					try
					{
						var deroulement = await _deroulementService.GetById(id);
						if(deroulement is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Détails d'un déroulement : Ressource inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource inexistante"));
						}

						var result = _mapper.Map<GetDeroulementRessource>(deroulement);
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

		[HttpGet("{id}/etapes")]
		public async Task<ActionResult> GetSteps(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Etapes d'un déroulement", TypeJournalId = 8, Entite = "User" };
					try
					{
						var deroulement = await _deroulementService.GetById(id);
						if (deroulement is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Liste des étapes d'un déroulement : Ressource inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource inexistante"));
						}

						var etapes = await _deroulementService.GetSteps(id);
						var result = _mapper.Map<IEnumerable<EtapeDeroulementRessource>>(etapes);
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						_logger.LogInformation("Liste des étapes d'un déroulement : Opération effectuée avec succès");
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
		public async Task<ActionResult> Add(DeroulementRessource ressource)
		{
			using(var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using(var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Enregistrement d'un déroulement", TypeJournalId = 5, Entite = "User" };
					try
					{
						var validation = new DeroulementRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Enregistrement d'un déroulement de prêt : Champs obligatoires");
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, description : "Champs obligaotoires"));
						}
						var typePret = await _typePretService.GetById(ressource.TypePretId);
						if(typePret is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogInformation("Enregistrement d'un déroulement d'un type de prêt : Type de prêt sélectionné introuvable");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Type de prêt sélectionné introuvable"));
						}
						var deroulement = _mapper.Map<Deroulement>(ressource);
						var deroulementCreated = await _deroulementService.Create(deroulement);
						var result = _mapper.Map<DeroulementRessource>(deroulementCreated);
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
						transaction.Commit();
						_logger.LogInformation("Enregistrement d'un déroulement de prêt : Opération effectuée avec succès");
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

		[HttpPut("update")]
		public async Task<ActionResult> Update(UpdateDeroulementRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Modification d'un déroulement", TypeJournalId = 5, Entite = "User" };
					try
					{
						var validation = new UpdateDeroulementRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Modification d'un déroulement de prêt : Champs obligatoires");
							return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Champs obligatoires"));
						}
						var deroulementDb = await _deroulementService.GetById(ressource.Id);
						if(deroulementDb is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Modification d'un déroulement de prêt : Ressource sélectionnée inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource sélectionnée inexistante"));
						}

						var deroulement = _mapper.Map<Deroulement>(ressource);
						var deroulementUpdated = await _deroulementService.Update(deroulementDb, deroulement);
						var result = _mapper.Map<DeroulementRessource>(deroulementUpdated);
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
						transaction.Commit();
						_logger.LogInformation("Modification d'un déroulement de prêt : Opération effectuée avec succès");
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
					var Journal = new Journal() { Libelle = "Suppression d'un déroulement", TypeJournalId = 4, Entite = "User" };
					try
					{
						var deroulement = await _deroulementService.GetById(id);
						if(deroulement is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Suppression d'un déroulement de prêt : Ressource sélectionnée inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource sélectionnée inexistante"));
						}
						var etapes = await _deroulementService.GetSteps(id);
						if(etapes.Count() > 0)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Suppression d'un déroulement de prêt : Impossible d'effectuer cette opération, veuillez supprimer les étapes un à un.");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Impossible d'effectuer cette opération, veuillez supprimer les étapes un à un."));
						}
						await _deroulementService.Delete(deroulement);
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
						transaction.Commit();
						_logger.LogWarning("Suppression d'un déroulement de prêt : Opération effectuée avec succès");
						
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
