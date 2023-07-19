using System.Runtime.CompilerServices;

namespace LoanManagement.API.Controllers.Loan_Management
{
	[ApiController]
	[Route("api/Loan/[controller]")]
	public class PretAccordController : Controller
	{
		private readonly IDossierClientService _dossierClientService;
		private readonly IEmployeurService _employeurService;
		private readonly IClientService _clientService;
		private readonly ICompteService _compteService;
		private readonly IStatutDossierClientService _statutDossierClientService;
		private readonly IDeroulementService _deroulementService;
		private readonly IPretAccordService _pretAccordService;
		private readonly IPeriodicitePaiementService _periodicitePaiementService;
		private readonly ITypePretService _typePretService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly JournalisationService _journalisationService;
		private readonly IConfiguration _configuration;
		

        public PretAccordController(IDossierClientService dossierClientService, IEmployeurService employeurService,
			IPretAccordService pretAccordService,IMapper mapper, 
			ILoggerManager logger, JournalisationService journalisationService,
			IConfiguration config, IPeriodicitePaiementService periodicitePaiementService,
			ITypePretService typePretService, IDeroulementService deroulementService, 
			IStatutDossierClientService statutDossierClientService, IClientService clientService, 
			ICompteService compteService)
        {
			_dossierClientService = dossierClientService;
			_pretAccordService = pretAccordService;
			_mapper = mapper;
			_logger = logger;
			_journalisationService = journalisationService;
			_configuration = config;
			_periodicitePaiementService = periodicitePaiementService;
			_typePretService = typePretService;
			_deroulementService = deroulementService;
			_statutDossierClientService = statutDossierClientService;
			_clientService = clientService;
			_employeurService = employeurService;
			_compteService = compteService;
        }

		[HttpPost("add")]
		public async Task<ActionResult> Add(PretAccordRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Montage d'un dossier crédit", TypeJournalId = 9, Entite = "User" };
					try
					{
						var validation = new PretAccordRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Montage d'un dossier crédit : Champs non conformes");
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, description: "Champs non conformes"));
						}
						var typePret = await _typePretService.GetById(ressource.TypePretId);
						var periodicitePaiement = await _periodicitePaiementService.GetById(ressource.PeriodicitePaiementId);
						var dossierClient = await _dossierClientService.GetById(ressource.DossierClientId);
						if(typePret is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Montage d'un dossier crédit : Type prêt sélectionné invalide");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description : "Type prêt sélectionné invalide")); 
						}
						if (periodicitePaiement is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Montage d'un dossier crédit : Périodicité de paiement sélectionné invalide");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Périodicité de paiement sélectionné invalide"));
						}
						if (dossierClient is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Montage d'un dossier crédit : Dossier crédit sélectionné invalide");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Dossier crédit sélectionné invalide"));
						}
						var deroulement = await _dossierClientService.GetDossierDeroulement(typePret.Id, ressource.MontantPret);
						var dossierCredit = _mapper.Map<PretAccord>(ressource);
						dossierCredit.QuotiteCessible = dossierCredit.Mensualite / 3;
						dossierCredit.PrimeTotale = dossierCredit.Surprime + dossierCredit.MontantPrime;
						dossierCredit.TauxEngagement = (int)((dossierCredit.SalaireNetMensuel * 100) / dossierCredit.Mensualite);
						var dossierCreditCreated = await _pretAccordService.Create(dossierCredit);
						var employeurId = await _employeurService.GetById(ressource.EmployeurId);
						if(employeurId is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Montage d'un dossier crédit : Employeur sélectionné invalide");

							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Employeur sélectionné invalide"));
						}
						if(deroulement is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Montage d'un dossier crédit : Aucune configuration initiale existante ne correspond au prêt en cours");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucue configuration ne correspond au prêt en cours"));
						}
						var etapeDeroulements = await _deroulementService.GetSteps(deroulement.Id);

						var status = new StatutDossierClient()
						{
							Date = DateTime.Now.ToString("dd/MM/yyyyy HH:mm:ss"),
							EtapeDeroulementId = etapeDeroulements.First().Id,
							DossierClientId = dossierClient.Id
						};
						var statutCreated = await _statutDossierClientService.Create(status);
						var result = _mapper.Map<PretAccordRessource>(dossierCreditCreated);
						Journal.Niveau = 2; //SUCCES
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Montage d'un dossier crédit : Opération effectuée avec succès");

						return Ok(result);

					}
					catch (Exception ex)
					{
						await transaction.RollbackAsync();
						Journal.Niveau = 1;
						await _journalisationService.Journalize(Journal);
						_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
						return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, 
							title: "Erreur interne du serveur", detail: ex.Message);
					}
				}
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(UpdatePretAccordRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Mise à jour du dossier crédit", 
						TypeJournalId = 5, Entite = "User" };
					try
					{
						var validation = new UpdatePretAccordRessourceValidator();
						var validationResult = await validation.ValidateAsync(ressource);
						if (!validationResult.IsValid)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Modification d'un dossier crédit : Champs incorrects");
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING,
								description : "Champs incorrects")); 
						}
						var x = await _pretAccordService.GetById(ressource.Id);

						if(x is null)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Modification d'un dossier crédit : Dossier crédit sélectionné introuvable");
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING,
								description: "Dossier crédit sélectionné introuvable"));
						}
						var typePret = await _typePretService.GetById(ressource.TypePretId);
						var periodicitePaiement = await _periodicitePaiementService.GetById(ressource.PeriodicitePaiementId);
						if (typePret is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Montage d'un dossier crédit : Type prêt sélectionné invalide");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Type prêt sélectionné invalide"));
						}
						if (periodicitePaiement is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Montage d'un dossier crédit : Périodicité de paiement sélectionné invalide");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Périodicité de paiement sélectionné invalide"));
						}
						var ressourceDb = _mapper.Map<PretAccord>(ressource);
						var update = await _pretAccordService.Update(ressourceDb, x);
						var result = _mapper.Map<PretAccordRessource>(update);
						Journal.Niveau = 2; //SUCCES
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

		[HttpGet("all")]
		public async Task<ActionResult> GetAll()
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal()
					{
						Libelle = "Liste des dossiers de crédit déjà montés",
						TypeJournalId = 8,
						Entite = "User"
					};
					try
					{
						var all = await _pretAccordService.GetAll();
						var result = _mapper.Map<IEnumerable<PretAccordRessource>>(all);
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

		[HttpPost("traiter")]
		public async Task<ActionResult> TraiterDossier(TraitementDossierRessource ressource)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Traitement d'un dossier client", TypeJournalId = 5, Entite = "User" };
					try
					{
						var dossierClient = await _dossierClientService.GetById(ressource.Id);
						if(dossierClient is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Traitement d'un dossier de crédit : Dossier sélectionné invalide");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Dossier crédit sélectionné invalide"));
						}
						var pretAccord = await _pretAccordService.GetPretAccordForDossier(dossierClient.Id);
						var deroulement = await _dossierClientService.GetDossierDeroulement(pretAccord.TypePretId,
							pretAccord.MontantPret);
						var statut = await _dossierClientService.GetStatut(dossierClient.Id);
						if(pretAccord is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Traitement d'un dossier client: Canevas sélectionné invalide");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Canevas sélectionné invalide"));
						}
						if(deroulement is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Traitement d'un dossier client: Déroulement introuvable pour le montant sélectionné");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description : "Déroulement introuvable pour le montant sélectionné"));
						}
						if(statut is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Traitement d'un dossier client: Aucun statut n'a été configuré pour ce dossier");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description : " Aucun statut n'a été configuré pour ce dossier"));
						}
						if(deroulement.NiveauInstance  > statut.EtapeDeroulementId + 1)
						{
							if(ressource.Response == false)
							{
								var newStatut = await _statutDossierClientService.Downgrade(statut, ressource.Motif);
								Journal.Niveau = 2; //SUCCES
								await _journalisationService.Journalize(Journal);
								_logger.LogInformation("Traitement d'un dossier de crédit : Dossier rejeté à l'étape précédente");
								return Ok();
							}
							else
							{
								var newStatut = await _statutDossierClientService.Upgrade(statut);
								Journal.Niveau = 2;
								await _journalisationService.Journalize(Journal);
								await transaction.CommitAsync();
								_logger.LogInformation("Traitement d'un dossier de crédit : Dossier relayé à l'étape suivante");
								return Ok();
							}
						}
						if(deroulement.NiveauInstance == statut.EtapeDeroulementId + 1)
						{
							if (ressource.Response == true)
							{
								await _statutDossierClientService.Assign(statut);
								Journal.Niveau = 2; //SUCCES
								await _journalisationService.Journalize(Journal);
								await transaction.CommitAsync();
								_logger.LogInformation("Traitement d'un dossier crédit : Demande de prêt accordé");

								return Ok();
							}
							else
							{
								await _statutDossierClientService.Reject(statut, ressource.Motif);
								Journal.Niveau = 2; //SUCCES
								await _journalisationService.Journalize(Journal);
								await transaction.CommitAsync();
								_logger.LogInformation("Traitement d'un dossier crédit : Demande de prêt rejeté");

								return Ok();
							}
						}
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

		[HttpPost("{id}/establish")]
		public async Task<ActionResult> Establishment(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Mettre en place le prêt sur le compte du client", TypeJournalId = 4, Entite = "User" };
					try
					{
						var dossier = await _dossierClientService.GetById(id);
						if(dossier is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Mise en place des fonds sur le compte du client : Dossier crédit sélectionné introuvable");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description : "Dossier crédit sélectionné introuvable")); 
						}
						var dossierCredit = await _pretAccordService.GetPretAccordForDossier(dossier.Id);
						var statutDossier = await _dossierClientService.GetStatut(dossier.Id);
						if(statutDossier.DecisionFinale is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Mise en place des fonds sur le compte du client : Prêt non approuvé pour le moment");
							return Unauthorized(new ApiResponse((int)CustomHttpCode.WARNING,
								description: "Prêt non approuvé pour le moment"));
						}
						var client = await _clientService.GetById(dossier.ClientId);
						var compte = await _clientService.GetCompte(client.Id);
						var compteUpdated = await _compteService.IncreaseAmount(compte, 
							dossierCredit.MontantPret);
						Journal.Niveau = 2; //SUCCES
						await transaction.CommitAsync();
						_logger.LogInformation("Mise en place des fonds sur le compte du client : Opération effectuée avec succès");
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
