using LoanManagement.core.Models.Loan_Management;
using LoanManagement.core.Models.Users_Management;

namespace LoanManagement.API.Controllers.Loan_Management
{
	[ApiController]
	[Route("api/Loan/[controller]")]
	public class TypePretController : Controller
	{
		private readonly ITypePretService _typePretService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly JournalisationService _journalisationService;
		private readonly IConfiguration _configuration;

		public TypePretController(ITypePretService typePretService, IMapper mapper, ILoggerManager logger,
			JournalisationService journalisationService, IConfiguration config)
		{
			_typePretService = typePretService;
			_mapper = mapper;
			_logger = logger;
			_journalisationService = journalisationService;
			_configuration = config;
		}

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<TypePretRessource>>> GetAll()
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var journal = new Journal() { Libelle = "Liste des types de prêt", TypeJournalId = 8, Entite = "User" };
					try
					{
						var result = await _typePretService.GetAll();
						var finalResult = _mapper.Map<IEnumerable<TypePretRessource>>(result);
						journal.Niveau = 3;
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

		[HttpGet("id")]
		public async Task<ActionResult> GetById(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var journal = new Journal() { Libelle = "Récupération d'un type de prêt par son id", Entite = "User", TypeJournalId = 6 };
					try
					{
						var typePret = await _typePretService.GetById(id);
						if (typePret is null)
						{
							_logger.LogWarning("Détails d'un type de prêt : Ressource introuvable");
							journal.Niveau = 1;
							await _journalisationService.Journalize(journal);
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Ressource introuvable"));
						}
						journal.Niveau = 3;
						await _journalisationService.Journalize(journal);
						_logger.LogInformation("Détails d'un type de prêt : Opération effectuée avec succès");
						var result = _mapper.Map<TypePretRessource>(typePret);

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
		public async Task<ActionResult> Add(string libelle)
		{
			using(var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using(var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Enregistrement d'un type de prêt", TypeJournalId = 5, Entite = "User" };
					try
					{
						if(libelle is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Enregistrement d'un type de prêt : Champ obligatoire");
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, description: "Champ obligatoire"));
						}
						var type = new TypePret() { Libelle = libelle };
						var typeDb = await _typePretService.Create(type);
						Journal.Niveau = 2;
						await _journalisationService.Journalize(Journal);
						transaction.Commit();
						_logger.LogInformation("Enregistrement d'un type de prêt : Opération effectuée avec succès");
						var result = _mapper.Map<TypePretRessource>(typeDb);

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

		[HttpGet("{id}/deroulements")]
		public async Task<ActionResult> GetDeroulements(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Déroulement d'un type de prêt", TypeJournalId = 8, Entite = "User" };
					try
					{
						var typePret = await _typePretService.GetById(id);
						if(typePret is null) 
						{
							Journal.Niveau = 1;
							try
							{
								await _journalisationService.Journalize(Journal);
								transaction.Commit();
							}
							catch (Exception ex)
							{
								await transaction.RollbackAsync();
								_logger.LogError("Une erreur est survenue pendant la journalisation de l'opération");
								return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
							}
							_logger.LogWarning("Déroulement d'un type de prêt : Type de prêt introuvable");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Type de prêt introuvable"));
							
						}

						var deroulements = await _typePretService.GetDeroulements(id);
						var result = _mapper.Map<IEnumerable<GetDeroulementRessource>>(deroulements);
						Journal.Niveau = 3;
						try
						{
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
						}
						catch (Exception ex)
						{
							await transaction.RollbackAsync();
							_logger.LogError("Une erreur est survenue pendant la journalisation de l'opération");
							return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
						}
						return Ok(result);
					}
					catch (Exception ex)
					{
						Journal.Niveau = 1;
						try
						{
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
						}
						catch (Exception ex1)
						{
							await transaction.RollbackAsync();
							_logger.LogError("Une erreur est survenue pendant la journalisation de l'opération");
							return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex1.Message);
						}
						_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
						return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
					}
				}
			}
		}
	}
}
