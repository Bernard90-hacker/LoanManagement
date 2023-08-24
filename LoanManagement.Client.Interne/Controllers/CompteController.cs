using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.Client.Interne.Controllers
{
    public class CompteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		[Route("Connexion")]
		public IActionResult Login()
		{

			TempData["Admin"] = "Admin";
			TempData["Gestionnaire"] = string.Empty;
			return View();
			
		}
	}
}
