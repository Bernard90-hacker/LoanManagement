using LoanManagement.service.Services.Loan_Management;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.API.Controllers.Loan_Management
{
    [ApiController]
    [Route("api/[controller]")]
	public class FicheAssuranceController : Controller
	{
        public FicheAssuranceService _ficheService { get; set; }
        public FicheAssuranceController(FicheAssuranceService ficheService)
        {
            _ficheService = ficheService;
        }
        [HttpPost("generate")]
        public ActionResult Generate()
		{
            _ficheService.GeneratePdf();
            return Ok();
		}
	}
}
