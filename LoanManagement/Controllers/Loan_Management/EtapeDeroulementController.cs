using CustomApiRessource.Enums;

namespace LoanManagement.API.Controllers.Loan_Management
{
	[ApiController]
	[Route("api/Loan/[controller]")]
	public class EtapeDeroulementController : Controller
	{
		private readonly IEtapeDeroulementService _etapeDeroulementService;
		private readonly IDeroulementService _deroulementService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly ITypeJournalService _typeJournalService;
		private readonly ITypePretService _typePretService;
		private readonly JournalisationService _journalisationService;
		private readonly IMembreOrganeService _membreOrganeService;
		private readonly IConfiguration _configuration;

		public EtapeDeroulementController(IEtapeDeroulementService etapeDeroulementService, IMapper mapper,
			ILoggerManager logger, JournalisationService journalisationService,
			IConfiguration config, ITypeJournalService typeJournalService,
			ITypePretService typePretService, IMembreOrganeService membreOrganeService,
			IDeroulementService deroulementService)
		{
			_etapeDeroulementService = etapeDeroulementService;
			_mapper = mapper;
			_typeJournalService = typeJournalService;
			_journalisationService = journalisationService;
			_configuration = config;
			_logger = logger;
			_typePretService = typePretService;
			_membreOrganeService = membreOrganeService;
			_deroulementService = deroulementService;
		}


		[HttpGet("all")]
		public async Task<ActionResult> GetAll()
		{

			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Liste des étapes de déroulement", TypeJournalId = 8, Entite = "User" };
					try
					{
						var all = await _etapeDeroulementService.GetAll();
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						var result = _mapper.Map<IEnumerable<EtapeDeroulementRessource>>(all);
						_logger.LogInformation("Liste des étapes de déroulement : Opération effectuée avec succès");
						
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
			using (var connection = new SqlConnection(_configuration.
				GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Identification d'une étape par son id", TypeJournalId = 6, Entite = "User" };
					try
					{
						var step = await _etapeDeroulementService.GetById(id);
						if(step is null)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Identification d'une étape par son id : Ressource introuvable");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Ressource introuvable"));
						}
						var result = _mapper.Map<EtapeDeroulementRessource>(step);
						Journal.Niveau = 3; //INFORMATION
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();

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
		public async Task<ActionResult> Add(EtapeDeroulementRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Enregistrement d'une étape", TypeJournalId = 5, Entite = "User" };
					try
					{
						var validation = new EtapeDeroulementRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Enregistrement d'une étape : Champs obligatoires");
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, 
								description: "Champs obligatoires"));
						}
						var membre = await _membreOrganeService.GetById(ressource.MembreOrganeId);
						if(membre is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogInformation("Enregistrement d'une étape : Membre sélectionné invalide");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Membre sélectionné invalide"));
						}
						var deroulement = await _deroulementService.GetById(ressource.DeroulementId);
						if(deroulement is null)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogInformation("Enregistrement d'une étape : Déroulement sélectionné invalide");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Déroulement sélectionné invalide"));
						}
						if(ressource.Etape > deroulement.NiveauInstance)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Enregistrement d'une étape : Le nombre d'étapes correspondant au déroulement a été atteint");
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, description : "Le nombre d'étapes correspondant au déroulement a été atteint")); 
						}
						var etape = _mapper.Map<EtapeDeroulement>(ressource);
						var etapeCreated = await _etapeDeroulementService.Create(etape);
						var result = _mapper.Map<EtapeDeroulementRessource>(etapeCreated);
						Journal.Niveau = 2; //SUCCES
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();

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

		[HttpGet("{id}/membre")]
		public async Task<ActionResult> GetMemberByStep(int id)
		{
			using(var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using(var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Identification du membre intervenant à une étape dans le processus de décision.", TypeJournalId = 8 };
                    try
					{
                        var etape = await _etapeDeroulementService.GetById(id);
                        if (etape is null)
                        {
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Identification du membre intervenant à une étape dans le processus de décision : Etape inexistant");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Etape inexistant."));
						}

						var membre = await _membreOrganeService.GetMembreByStep(id);
						Journal.Niveau = 3;
						await _journalisationService.Journalize(Journal);
						_logger.LogWarning("Identification du membre intervenant à une étape dans le processus de décision : Opération effectuée avec succès");
						return Ok(membre);
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

		[HttpPut("update")]
		public async Task<ActionResult> Update(UpdateEtapeDeroulementRessource ressource)
		{

			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Mise à jour d'une étape", TypeJournalId = 3, Entite = "User" };
					try
					{
						var validation = new UpdateEtapeDeroulementRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Mise à jour d'une étape : Champs obligatoires");
							return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Champs obligatoires"));
						}
						var etapeExist = await _etapeDeroulementService.GetById(ressource.Id);
						var deroulement = await _deroulementService.GetById(ressource.DeroulementId);
						var membre = await _membreOrganeService.GetById(ressource.MembreOrganeId);
						if(deroulement is null || membre is null || etapeExist is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Mise à jour d'une étape : Membre ou deroulement ou etape sélectionné invalide(s)");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Membre ou déroulement sélectionné ou étape invalide(s)"));
						}
						var etape = _mapper.Map<EtapeDeroulement>(ressource);
						var etapeUpdated = await _etapeDeroulementService.Update(etapeExist, etape);
						var result = _mapper.Map<EtapeDeroulementRessource>(etapeUpdated);
						Journal.Niveau = 2; //SUCCES
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Mise à jour d'une étape : Opération effectuée avec succès");

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
					var Journal = new Journal() { Libelle = "Suppression d'une étape", TypeJournalId = 4, 
						Entite = "User" };
					try
					{
						var etape = await _etapeDeroulementService.GetById(id);
						if(etape is null)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Suppression d'une étape : Ressource inexistante");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Ressource inexistante"));
						}
						Journal.Niveau = 2; //SUCCES
						await _etapeDeroulementService.Delete(etape);
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Suppression d'une étape : Opération effectuée avec succès");
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
