using LoanManagement.service.Services.Loan_Management;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.API.Controllers.Loan_Management
{
    [ApiController]
    [Route("api/[controller]")]
	public class FicheAssuranceController : Controller
	{
        private readonly FicheAssuranceService _ficheService;
        private readonly IDossierClientService _dossierClientService;
		private readonly IConfiguration _configuration;
		private readonly JournalisationService _journalisationService;
		private readonly ILoggerManager _logger;
        public FicheAssuranceController(FicheAssuranceService ficheService, 
            IDossierClientService dossierClientService, ILoggerManager logger,
			IConfiguration configuration, JournalisationService journalisationService)
        {
            _ficheService = ficheService;
            _dossierClientService = dossierClientService;
			_configuration = configuration;
			_journalisationService = journalisationService;
			_logger = logger;
        }
        [HttpPost("generate")]
        public async Task<ActionResult> Generate(int id)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					var Journal = new Journal() { Libelle = "Génération d'une fiche d'assurance", TypeJournalId = 10, Entite = "Client" };
					try
					{
						var dossier = await _dossierClientService.GetById(id);
						if (dossier == null)
						{
							Journal.Niveau = 1; //ECHEC
							await _journalisationService.Journalize(Journal);
							await transaction.CommitAsync();
							_logger.LogWarning("Génération d'une fiche d'assurance : Dossier crédit sélectionné inexistant");

							return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description:
								"Dossier crédit sélectionné inexistant"));
						}
						await _ficheService.GeneratePdf(dossier);
						Journal.Niveau = 2; //SUCCES
						await _journalisationService.Journalize(Journal);
						await transaction.CommitAsync();
						_logger.LogInformation("Génération de la fiche d'assurance : Opération effectuée avec succès");

						return Ok();

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
