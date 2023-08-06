using IdentityServer4.Models;
using LoanManagement.Client.Resources;
using LoanManagement.Client.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.IO.Pipelines;

namespace LoanManagement.Client.Controllers
{
    [Route("DemandeCredit")]
    public class DemandeCreditController : Controller
    {
        private readonly ILogger<DemandeCreditController> _logger;
        private readonly IConfiguration _config;
        private readonly JournalisationService _journalisationService;
        private readonly HttpClient Client = new HttpClient();
        public DemandeCreditController(ILogger<DemandeCreditController> logger, 
            IConfiguration config, JournalisationService service)
        {
            _logger = logger;
            _config = config;
            _journalisationService = service;
        }

        private string UrlBase => MyConstants.LoanManagementApiUrl;
        [HttpGet]
        public IActionResult DemandeCredit()
        {
            if (HttpContext.Session.GetString("_SESSIONID").IsNull())
                return RedirectToAction("Index", "Home");
            ConfigConstant.AddCurrentRouteToSession(HttpContext);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DemandeCreditAction(SaveDossierClientResourceViewModel model)
        {
            if (!ModelState.IsValid)
                return new JsonResult(new
                { }, ConfigConstant.JsonSettings())
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            try
            {
                if (HttpContext.Session.GetString("_SESSIONID").IsNull())
                    return RedirectToAction("Index", "Home");

                List<IFormFile> files = new() 
                { 
                    model.SaveDossierClientResource.AttestationTravail, 
                    model.SaveDossierClientResource.ContratTravail, 
                    model.SaveDossierClientResource.PremierBulletinSalaire, 
                    model.SaveDossierClientResource.DeuxiemeBulletinSalaire, 
                    model.SaveDossierClientResource.TroisiemeBulletinSalaire, 
                    model.SaveDossierClientResource.FactureProFormat 
                };
                var content = new MultipartFormDataContent();
                int i = 0;
                foreach (var file in files)
                {
                    byte[] data;
                    using (var br = new BinaryReader(file.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)file.OpenReadStream().Length);
                    }
                    var bytes = new ByteArrayContent(data);
                    switch (i)
                    {
                        case 0:
                            content.Add(bytes, "attestationTravail", file.FileName);
                            break;
                        case 1:
                            content.Add(bytes, "contratTravail", file.FileName);
                            break;
                        case 2:
                            content.Add(bytes, "premierBulletinSalaire", file.FileName);
                            break;
                        case 3:
                            content.Add(bytes, "deuxiemeBulletinSalaire", file.FileName);
                            break;
                        case 4:
                            content.Add(bytes, "troisiemeBulletinSalaire", file.FileName);
                            break;
                        case 5:
                            content.Add(bytes, "factureProFormat", file.FileName);
                            break;
                        default:
                            break;
                    }
                    i++;
                }

                var dossier = new SaveDossierClientResource()
                {
                    Taille = model.SaveDossierClientResource.Taille,
                    Poids = model.SaveDossierClientResource.Poids,
                    TensionArterielle = model.SaveDossierClientResource.TensionArterielle,
                    Fumeur = model.SaveDossierClientResource.Fumeur,
                    NbrCigarettes = model.SaveDossierClientResource.NbrCigarettes,
                    Distractions = model.SaveDossierClientResource.Distractions,
                    EstSportif = model.SaveDossierClientResource.EstSportif,
                    EstInfirme = model.SaveDossierClientResource.EstInfirme,
                    CategorieSport = model.SaveDossierClientResource.CategorieSport,
                    NatureInfirmite = model.SaveDossierClientResource.NatureInfirmite,
                    DateSurvenance = model.SaveDossierClientResource.DateSurvenance,
                    AttestationTravail = model.SaveDossierClientResource.AttestationTravail,
                    ContratTravail = model.SaveDossierClientResource.ContratTravail,
                    PremierBulletinSalaire = model.SaveDossierClientResource.PremierBulletinSalaire,
                    DeuxiemeBulletinSalaire = model.SaveDossierClientResource.DeuxiemeBulletinSalaire,
                    TroisiemeBulletinSalaire = model.SaveDossierClientResource.TroisiemeBulletinSalaire,
                    FactureProFormat = model.SaveDossierClientResource.FactureProFormat,
                    CarteIdentite = model.SaveDossierClientResource.CarteIdentite,
                    EcheanceCarteIdentite = model.SaveDossierClientResource.EcheanceCarteIdentite,
                    ClientId = int.Parse(HttpContext.Session.GetString("_SESSIONID")),
                    StatutMaritalId = model.SaveDossierClientResource.StatutMaritalId
                };
               
                var journal = await _journalisationService.Journalize();
               content.Add(new StringContent(JsonConvert.SerializeObject(dossier), Encoding.UTF8, "application/json"));

                Client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                Client.DefaultRequestHeaders.Add("X-Journalisation", JsonConvert.SerializeObject(journal));
                var response = await Client.PostAsync(UrlBase + "/DossierClient/Add", content);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.IsApplySuccess = true;
                    return new JsonResult(new
                    {
                        title = "Demande de crédit",
                        typeMessage = TypeMessage.Success.GetString(),
                        message = "Demande de crédit effectuée avec succès",
                        description = string.Empty,
                        timeOut = 8000,
                        strJsonDemandeCredit = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<SaveDossierClientResource>(
                        await response.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                    }, ConfigConstant.JsonSettings())
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                var jQueryViewModel = await AppConstant.GetResponseMessage(response);
                return new JsonResult(new
                {
                    title = jQueryViewModel.Title,
                    typeMessage = jQueryViewModel.TypeMessage,
                    message = jQueryViewModel.Message,
                    description = jQueryViewModel.Description,
                    erreurs = jQueryViewModel.Errors,
                    timeOut = jQueryViewModel.TimeOut


                }); 
            }
            catch (Exception)
            {

                throw new Exception("Quelque chose s'est mal passé");

            }
        }
    }
}
