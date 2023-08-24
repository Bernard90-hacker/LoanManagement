using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.Customer.Controllers
{
    public class CompteController : Controller
    {
        private readonly ILogger<CompteController> _logger;
        private readonly IConfiguration _config;
        private readonly IMemoryCache _memoryCache;
        private readonly HttpClient Client = new HttpClient();

        public CompteController(ILogger<CompteController> logger, IConfiguration config, IMemoryCache memoryCache)
        {
            _logger = logger;
            _config = config;
            _memoryCache = memoryCache;
        }


        [Route("Connexion")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
