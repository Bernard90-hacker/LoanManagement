using Constants.Config;
using FluentValidation;
using LoanManagement.API.Ressources.Loan_Management;
using LoanManagement.core.Models.Users_Management;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

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
		private readonly IWebHostEnvironment _webHostEnvironment;
		public DossierClientController(IDossierClientService dossierClientService, IMapper mapper,
			ILoggerManager logger, JournalisationService journalisationService, 
			IConfiguration configuration, ISanteClientService infoSanteClientService,
			IStatutDossierClientService statutDossierClientService, IStatutMaritalService statutMaritalService,
			IClientService clientService, INatureQuestionService natureQuestionService, IWebHostEnvironment env)
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
			_webHostEnvironment = env;
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
						var result = _mapper.Map<IEnumerable<SaveDossierClientResource>>(all);
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

		[HttpGet("closed")]
		public async Task<ActionResult> GetClosed()
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal()
					{
						Libelle = "Liste des demandes de prêts clôturés",
						TypeJournalId = 8,
						Entite = "User"
					};
					try
					{
						var closed = await _dossierClientService.GetClosed();
						var result = _mapper.Map<IEnumerable<SaveDossierClientResource>>(closed);
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
						var result = _mapper.Map<SaveDossierClientResource>(dossier);

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

		[HttpGet("Client/{id}")]
		public async Task<ActionResult> GetByClientId(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var journal = new Journal() { Libelle = "Identification d'un dossier crédit par son id", Entite = "User", TypeJournalId = 6 };
					try
					{
						var client = await _clientService.GetById(id);
						if (client is null)
						{
							_logger.LogWarning("Détails d'un dossier crédit : Client introuvable");
							journal.Niveau = 1; // ECHEC
							await _journalisationService.Journalize(journal);
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Client introuvable"));
						}
						var dossier = await _dossierClientService.GetByClientId(client.Id);
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
		public async Task<ActionResult> Add([FromForm] SaveDossierClientResource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal();
					if (Request.Headers.ContainsKey("X-Journalisation"))
					{
						var x = Request.Headers["X-Journalisation"];
						Journal = JsonConvert.DeserializeObject<Journal>(x);
					}

					Journal.Libelle = "Enregistrement d'une demande de prêt";
					Journal.TypeJournalId = 5;
					Journal.Entite = "Client";
					
					try
					{
						var validation = new SaveDossierClientResourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Enregistrement d'une demande de prêt : Champs incorrects.");
							return BadRequest();
						}
						var statutMarital = await _statutMaritalService.GetById(ressource.StatutMaritalId);
						var client = await _clientService.GetById(ressource.ClientId);
						if(statutMarital is null || client is null)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Enregistrement d'une demande de prêt : " +
								"Statut marital ou client sélectionné invalide(s)");

							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Statut marital ou client sélectionné invalide (s)"));
						}
						var dossier = _mapper.Map<DossierClient>(ressource);
						dossier.ClientId = ressource.ClientId;
						dossier.Cloturer = false;
						dossier.DateSoumission = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                        dossier.NumeroDossier = DateTime.Now.ToString("ddMMyyyyhh:mm:ss");
                        if (!ressource.ContratTravail.IsNull())
                        {
                            dossier.ContratTravail = await ConfigConstants.UploadPdfFile(_webHostEnvironment, ressource.ContratTravail, GlobalConstants.DossierCredit);
                            if (dossier.ContratTravail.Equals("NotAccepted"))
                            {
                                _logger.LogWarning("'Enregistrement d'un dossier crédit' : format du contrat de travail non valide.");
                                return NotFound(new ApiResponse((int)CustomHttpCode.ERROR, description: "Format du contrat de travail invalide !! Seuls sont autorisés les formats png, jpeg ou jpg."));
                            }
                        }
                        if (!ressource.AttestationTravail.IsNull())
                        {
                            dossier.AttestationTravail = await ConfigConstants.UploadPdfFile(_webHostEnvironment, ressource.ContratTravail, GlobalConstants.DossierCredit);
                            if (dossier.AttestationTravail.Equals("NotAccepted"))
                            {
                                _logger.LogWarning("'Enregistrement d'un dossier crédit' : format de l'attestation de travail non valide.");
                                return NotFound(new ApiResponse((int)CustomHttpCode.ERROR, description: "Format de l'attestation de travail invalide !! Seuls sont autorisés les formats pdf."));
                            }
                        }
                        if (!ressource.PremierBulletinSalaire.IsNull())
                        {
                            dossier.PremierBulletinSalaire = await ConfigConstants.UploadPdfFile(_webHostEnvironment, ressource.ContratTravail, GlobalConstants.DossierCredit);
                            if (dossier.PremierBulletinSalaire.Equals("NotAccepted"))
                            {
                                _logger.LogWarning("'Enregistrement d'un dossier crédit' : format des bulletins de salaire non valide.");
                                return NotFound(new ApiResponse((int)CustomHttpCode.ERROR, description: "Format des bulletins de salaire de travail invalide !! Seuls sont autorisés les formats pdf."));
                            }
                        }
                        if (!ressource.DeuxiemeBulletinSalaire.IsNull())
                        {
                            dossier.DeuxiemeBulletinSalaire = await ConfigConstants.UploadPdfFile(_webHostEnvironment, ressource.ContratTravail, GlobalConstants.DossierCredit);
                            if (dossier.PremierBulletinSalaire.Equals("NotAccepted"))
                            {
                                _logger.LogWarning("'Enregistrement d'un dossier crédit' : format des bulletins de salaire non valide.");
                                return NotFound(new ApiResponse((int)CustomHttpCode.ERROR, description: "Format des bulletins de salaire de travail invalide !! Seuls sont autorisés les formats pdf."));
                            }
                        }
                        if (!ressource.TroisiemeBulletinSalaire.IsNull())
                        {
                            dossier.TroisiemeBulletinSalaire = await ConfigConstants.UploadPdfFile(_webHostEnvironment, ressource.ContratTravail, GlobalConstants.DossierCredit);
                            if (dossier.PremierBulletinSalaire.Equals("NotAccepted"))
                            {
                                _logger.LogWarning("'Enregistrement d'un dossier crédit' : format des bulletins de salaire non valide.");
                                return NotFound(new ApiResponse((int)CustomHttpCode.ERROR, description: "Format des bulletins de salaire de travail invalide !! Seuls sont autorisés les formats pdf."));
                            }
                        }
                        if (!ressource.FactureProFormat.IsNull())
                        {
                            dossier.FactureProFormat = await ConfigConstants.UploadPdfFile(_webHostEnvironment, ressource.ContratTravail, GlobalConstants.DossierCredit);
                            if (dossier.FactureProFormat.Equals("NotAccepted"))
                            {
                                _logger.LogWarning("'Enregistrement d'un dossier crédit' : format de la facture proformat non valide.");
                                return NotFound(new ApiResponse((int)CustomHttpCode.ERROR, description: "Format des bulletins de salaire de travail invalide !! Seuls sont autorisés les formats png, jpeg ou jpg."));
                            }
                        }
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

		[HttpPost("couverture")]
		public async Task<ActionResult> AddCouverture(int dossierId, [FromForm] CouvertureRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal()
					{
						Libelle = "Ajout de la fiche couverture emprunteur",
						TypeJournalId = 5,
						Entite = "Client"
					};
					try
					{
						var dossier = await _dossierClientService.GetById(dossierId);
						if(dossier is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							_logger.LogWarning("Ajout de la fiche de couverture : Dossier sélectionné invalide.");

							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Dossier sélectionné invalide."));
						}
                        if (!ressource.Couverture.IsNull())
                        {
                            string couverture = await ConfigConstants.UploadPdfFile(_webHostEnvironment, ressource.Couverture, GlobalConstants.PhotoUtilisateur);
                            if (couverture.Equals("NotAccepted"))
                            {
                                _logger.LogWarning("'Enregistrement d'un dossier crédit' : format de fichier non valide.");
                                return NotFound(new ApiResponse((int)CustomHttpCode.ERROR, description: "Format de fichier invalide !! Seuls sont autorisés les formats pdf."));
                            }
                            await _dossierClientService.AddCouverture(dossier, couverture);
                        }
						Journal.Niveau = 2; //SUCCES
						await _journalisationService.Journalize(Journal);
						_logger.LogInformation("Ajout de la couverture d'emprunteur : Opération effectuée avec succès.");
						return Ok();
					}
					catch (Exception ex)
					{
                        await transaction.RollbackAsync();
                        _logger.LogError("Une erreur est survenue pendant le traitement de la requête");
                        return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
                    }
				}
			}
		}
		[HttpPut("{id}/cloturer")]
		public async Task<ActionResult> Cloturer(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Clôture d'un dossier crédit", TypeJournalId = 3, Entite = "User" };
					try
					{
						var dossier = await _dossierClientService.GetById(id);
						if(dossier is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Cloture d'un dossier crédit : Dossier inexistant");

							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Dossier inexistant"));
						}
						Journal.Niveau = 2; //SUCCES
						await _dossierClientService.Cloturer(dossier);
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Clôture d'un dossier : Opération effectuée avec succès");

						return Ok();
					}
					catch (Exception ex)
					{
						await transaction.RollbackAsync();
						_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
						return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
					}
				}
			}
		}
		[HttpPost("addInfoSanteClient")]
		public async Task<ActionResult> Add(List<InfoSanteClientRessource> ressource)
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
						foreach (var item in ressource)
						{
                            var validationResult = await validation.ValidateAsync(item);
                            if (!validationResult.IsValid)
                            {
                                Journal.Niveau = 1; //ECHEC
                                await _journalisationService.Journalize(Journal);
                                await transaction.CommitAsync();

                                _logger.LogWarning("Enregistrement des informations liées à la santé du client : Champs obligatoires");
                                return BadRequest(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
                                    description: "Champs obligatoires"));
                            }
                        }
						
						//var dossierClient = await _dossierClientService.GetById(ressource.DossierClientId);
						//var question = await _natureQuestionService.GetById(ressource.NatureQuestionId);
						//if(dossierClient is null || question is null)
						//{
						//	Journal.Niveau = 1; //ECHEC
						//	await _journalisationService.Journalize(Journal);
						//	await transaction.CommitAsync();
						//	_logger.LogWarning("Enregistrement des informations liées à la santé du client : Statut marital ou client sélectionné invalide(s)");

						//	return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
						//		description: "Statut marital ou client sélectionné invalide (s)"));
						//}
						var infoSante = _mapper.Map<List<InfoSanteClient>>(ressource);
						var infoSanteCreated = await _infoSanteClientService.Create(infoSante);
						var result = _mapper.Map<List<InfoSanteClientRessource>>(infoSante);
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
