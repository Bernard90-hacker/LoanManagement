using Constants.Pagination;
using Microsoft.AspNetCore.Http.Extensions;
using Shyjus.BrowserDetection;
using Wangkanai.Detection.Services;

namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class JournalController : Controller
	{
		private readonly IJournalService _journalService;
		private readonly ITypeJournalService _typeJournalService;
		private readonly IMapper _mapper;
		private readonly ILoggerManager _logger;
		private readonly IUtilisateurService _utilisateurService;

        public JournalController(IJournalService journalService, IMapper mapper, 
			ILoggerManager logger, ITypeJournalService typeJournalService, 
			IUtilisateurService utilisateurService)
        {
			_journalService = journalService;
			_mapper = mapper;
			_logger = logger;
			_typeJournalService = typeJournalService;
			_utilisateurService = utilisateurService;
        }

		[HttpGet("all")]
		public async Task<ActionResult<PagedList<JournalRessource>>> GetAll([FromQuery] JournalParameters parameters)
		{
			try
			{
				var journaux = await _journalService.GetAll(parameters);
				var types = await _typeJournalService.GetAll();
				if(journaux.Count() == 0)
				{
					_logger.LogWarning("Liste des journaux : Aucun journal n'a été trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun journal n'a été trouvé"));
				}

				var metadata = new
				{
					journaux.PageSize,
					journaux.CurrentPage,
					journaux.TotalCount,
					journaux.TotalPages,
					journaux.HasNext,
					journaux.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				var result = new List<JournalRessource>();
				var user = new Utilisateur();
				var query = from j in journaux
							from t in types
							where j.TypeJournalId == t.Id
							select (j.Niveau, t.Libelle, t.Code, 
							_utilisateurService.GetUserById(j.UtilisateurId).Result.Username);


				_logger.LogInformation("Liste des journaux : Opération effectuée avec succès");
				return Ok(query);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult<JournalRessource>> Add(JournalRessource ressource)
		{
			try
			{
				var validation = new JournalValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Journalisation d'un événement : Champs obligatoires");
					return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, description: "Champs obligatoires"));
				}
				var journalCodeExist = await _typeJournalService.GetTypeJournalByCode(ressource.TypeJournalCode);
				var userExists = await _utilisateurService.GetUserByUsername(ressource.Username);
				if (journalCodeExist is null)
				{
					_logger.LogWarning("Journalisation d'un événement: Type journal associé inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Type journal associé inexistant"));
				}
				if(userExists is null)
				{
					_logger.LogWarning("Journalisation d'un événement: Utilisateur inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur inexistant"));
				}
				var journalDb = _mapper.Map<Journal>(ressource);
				Uri adress = new Uri(Request.Host.ToString());
				journalDb.PageURL = Request.GetDisplayUrl();
				journalDb.PreferenceURL = "preference";
				journalDb.TypeJournalId = journalCodeExist.Id;
				journalDb.UtilisateurId = userExists.Id;

				var journalCreated = await _journalService.Create(journalDb);
				
				var result = _mapper.Map<JournalRessource>(journalCreated);
				_logger.LogInformation("Journalisation d'un évènement : Opération effectuée avec succès");

				return Ok(result);

			}
			catch (Exception ex)
			{
				
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

    }
}
