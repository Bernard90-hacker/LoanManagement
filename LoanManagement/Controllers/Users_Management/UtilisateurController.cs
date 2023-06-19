namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class UtilisateurController : Controller
	{
		private readonly IUtilisateurService _utilisateurService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly IConfiguration _config;

        public UtilisateurController(UtilisateurService utilisateurService, ILoggerManager logger, 
			IMapper mapper, IConfiguration config)
        {
			_utilisateurService = utilisateurService;
			_logger = logger;
			_mapper = mapper;
			_config = config;
        }

		[HttpPost("register")]
		public Task<ActionResult<UtilisateurRessource>> Register(UtilisateurRessource ressource)
		{

		}
    }
}
