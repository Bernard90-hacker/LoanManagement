using System.Runtime.CompilerServices;

namespace LoanManagement.API.Controllers.Loan_Management
{
	[ApiController]
	[Route("api/Loan/[controller]")]
	public class PretAccordController : Controller
	{
		private readonly IDossierClientService _dossierClientService;
		private readonly IEtapeDeroulementService _etapeDeroulementService;
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
		private readonly EmailService _mailService;
		

        public PretAccordController(IDossierClientService dossierClientService, IEmployeurService employeurService,
			IPretAccordService pretAccordService,IMapper mapper, 
			ILoggerManager logger, JournalisationService journalisationService,
			IConfiguration config, IPeriodicitePaiementService periodicitePaiementService,
			ITypePretService typePretService, IDeroulementService deroulementService, 
			IStatutDossierClientService statutDossierClientService, IClientService clientService, 
			ICompteService compteService, IEtapeDeroulementService service, EmailService mailService)
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
			_etapeDeroulementService = service;
			_mailService = mailService;
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
                        var employeurId = await _employeurService.GetById(ressource.EmployeurId);
                        if (employeurId is null)
                        {
                            Journal.Niveau = 1;
                            await _journalisationService.Journalize(Journal);
                            await transaction.CommitAsync();
                            _logger.LogWarning("Montage d'un dossier crédit : Employeur sélectionné invalide");

                            return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
                                description: "Employeur sélectionné invalide"));
                        }
                        if (deroulement is null)
                        {
                            Journal.Niveau = 1;
                            await _journalisationService.Journalize(Journal);
                            await transaction.CommitAsync();
                            _logger.LogWarning("Montage d'un dossier crédit : Aucune configuration initiale existante ne correspond au prêt en cours");
                            return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucune configuration ne correspond au prêt en cours"));
                        }
                        var dossierCredit = _mapper.Map<PretAccord>(ressource);
						dossierCredit.QuotiteCessible = dossierCredit.Mensualite / 3;
						dossierCredit.PrimeTotale = dossierCredit.Surprime + dossierCredit.MontantPrime;
						dossierCredit.TauxEngagement = (int)((dossierCredit.SalaireNetMensuel) / dossierCredit.Mensualite);
						var dossierCreditCreated = await _pretAccordService.Create(dossierCredit);
						
						var etapeDeroulements = await _deroulementService.GetSteps(deroulement.Id);

						var status = new StatutDossierClient()
						{
							Date = DateTime.Now.ToString("dd/MM/yyyyy HH:mm:ss"),
							DateReception = DateTime.Now.ToString("dd/MM/yyyyy HH:mm:ss"),
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
						var client = new Client();
						var dossier = new DossierClient();
                        foreach (var item in result)
                        {
							dossier = await _dossierClientService.GetById(item.DossierClientId);
							client = await _clientService.GetById(dossier.ClientId);
							item.NomClient = $"{client.Nom} {client.Prenoms}";
                        }
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
					var Journal = new Journal() { Libelle = "Traitement d'un dossier crédit", TypeJournalId = 5, Entite = "User" };
					try
					{
						//var dossierClient = await _dossierClientService.GetById(ressource.Id);
						
						var dossierClient = await _pretAccordService.GetPretAccordForDossier(ressource.Id);
						var client = await _clientService.GetById(dossierClient.ClientId);
						if (dossierClient is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Traitement d'un dossier de crédit : Dossier sélectionné invalide");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Dossier crédit sélectionné invalide"));
						}
						var pretAccord = await _pretAccordService.GetPretAccord(dossierClient.Id);
						var deroulement = await _dossierClientService.GetDossierDeroulement(pretAccord.TypePretId,
							pretAccord.MontantPret);
						var statut = await _dossierClientService.GetStatut(dossierClient.Id);
						if(dossierClient is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Traitement d'un dossier crédit: Canevas sélectionné invalide");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description: "Canevas sélectionné invalide"));
						}
						if(deroulement is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Traitement d'un dossier crédit: Déroulement introuvable pour le montant sélectionné");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description : "Déroulement introuvable pour le montant sélectionné"));
						}
						if(statut is null)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Traitement d'un dossier crédit: Aucun statut n'a été configuré pour ce dossier");
							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND,
								description : " Aucun statut n'a été configuré pour ce dossier"));
						}
						if (dossierClient.Cloturer)
						{
							Journal.Niveau = 3; //INFORMATION : Le dossier	a été clôturé;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Traitement d'un dossier crédit : Le dossier a été clôturé");

							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING,
								description : "Le dossier a été clôturé"));
						}
						if (dossierClient.DossierTraite)
						{
							Journal.Niveau = 3; //INFORMATION : Le dossier	a été déjà traité;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Traitement d'un dossier crédit : Le dossier a été déjà traité");

							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING,
								description: "Le dossier a été traité préalablement"));
						}
						if (deroulement.NiveauInstance  > statut.EtapeDeroulementId + 1)
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
						var etape = await _etapeDeroulementService.GetById(statut.EtapeDeroulementId);
						if(deroulement.NiveauInstance == etape.Etape)
						{
							if (ressource.Response == true)
							{
								await _statutDossierClientService.Assign(statut);
								await _mailService.NotifyClient(true, client.Email, dossierClient.Montant);
								await _dossierClientService.Cloturer(dossierClient);
								Journal.Niveau = 2; //SUCCES
								await _journalisationService.Journalize(Journal);
								await transaction.CommitAsync();
								_logger.LogInformation("Traitement d'un dossier crédit : Demande de prêt accordé");

								return Ok();
							}
							else
							{
								await _statutDossierClientService.Reject(statut, ressource.Motif);
								await _mailService.NotifyClient(false, client.Email, dossierClient.Montant);
								await _dossierClientService.Cloturer(dossierClient);
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
		public async Task<ActionResult> Establish(int id)
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
						if(statutDossier.DecisionFinale is null || statutDossier.DecisionFinale == false)
						{
							Journal.Niveau = 1;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Mise en place des fonds sur le compte du client : Prêt non approuvé");
							return Unauthorized(new ApiResponse((int)CustomHttpCode.WARNING,
								description: "Prêt non approuvé"));
						}
						if (dossier.Cloturer)
						{
							Journal.Niveau = 3;
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Mise en place des fonds sur le compte du client : Le dossier a déja été clôturé");
							
							return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING,
								description: "Le dossier a déja été clôturé"));
						}
					
						var client = await _clientService.GetById(dossier.ClientId);
						var compte = await _clientService.GetCompte(client.Id);
						var compteUpdated = await _compteService.IncreaseAmount(compte, 
							dossierCredit.Montant); //On effectue la mise en place sur le compte du client.
						await _dossierClientService.Cloturer(dossier); // On cloture le dossier;
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
