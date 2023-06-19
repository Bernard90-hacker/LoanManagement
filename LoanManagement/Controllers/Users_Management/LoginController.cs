namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/Users/[controller]")]
	public class LoginController : Controller
	{
		private IUtilisateurService _utilisateurService;
        public LoginController(IUtilisateurService utilisateurService)
        {
			_utilisateurService = utilisateurService;
        }
    }
}
