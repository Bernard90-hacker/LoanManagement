namespace LoanManagement.Client.Interne.Controllers
{
    [Route("")]
    public class GestionnaireController : Controller
    {
        private readonly ILogger<GestionnaireController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient Client = new HttpClient();

        public GestionnaireController(ILogger<GestionnaireController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        [Route("Gestionnaire")]
        public IActionResult Gestionnaire()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);
            return View();
        }

        [HttpGet]
        [Route("DossierCredit")]
        public IActionResult DossierCredit()
        {

            return View();
        }

        [HttpGet]
        [Route("Employeur")]
        public IActionResult Employeur()
        {

            return View();
        }

        [HttpGet]
        [Route("ListeDossierCredit")]
        public IActionResult ListeDossierCredit()
        {

            return View();
        }
    }
}
