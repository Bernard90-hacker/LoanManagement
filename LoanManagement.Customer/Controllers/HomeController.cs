using LoanManagement.Customer.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LoanManagement.Customer.Controllers;

/// <summary>
/// 
/// </summary>
[Route("")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _config;
    private readonly IMemoryCache _memoryCache;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly HttpClient Client = new HttpClient();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="config"></param>
    /// <param name="memoryCache"></param>
    public HomeController(ILogger<HomeController> logger,
        IConfiguration config, IMemoryCache memoryCache, IWebHostEnvironment web)
    {
        _logger = logger;
        _config = config;
        _memoryCache = memoryCache;
        _webHostEnvironment = web;
    }

    private string URL => MyConstants.LoanManagementApiUrl;

    public IActionResult Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("_SESSIONID")))
            return RedirectToAction("Login", "Compte");

        return View();
    }

    [Route("Azerty")]
    public IActionResult Azerty()
    {
        return View();
    }

    [Route("Querty")]
    public IActionResult Querty()
    {
        return View();
    }

    [Route("MesDemandes")]
    public async Task<IActionResult> MesDemandes()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("_SESSIONID")))
            return RedirectToAction("Login", "Compte");

        return View();
    }

    [Route("Demandes")]
    public async Task<IActionResult> Demandes()
    {
        var id = int.Parse(HttpContext.Session.GetString("_SESSIONID"));
        var demandes = await Client.GetAsync(URL + $"/DossierClient/Client/{id}");
        if (demandes.IsSuccessStatusCode)
        {
            return new JsonResult(new
            {
                title = "Demandes",
                typeMessage = TypeMessage.Success.GetString(),
                message = "Liste des demandes",
                description = string.Empty,
                timeOut = 8000,
                strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<IEnumerable<GetDossierClientResource>>(
                      await demandes.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
            }, ConfigConstant.JsonSettings())
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        var jQueryViewModel = await AppConstant.GetResponseMessage(demandes);
        return new JsonResult(new
        {
            title = "Liste des demandes",
            typeMessage = TypeMessage.Error.GetString(),
            message = "Une erreur s'est produite",
            description = "Vos identifiants sont incorrects",
            strJsonLogin = JsonConvert.SerializeObject(jQueryViewModel),
            timeOut = jQueryViewModel.TimeOut

        });
    }

    [Route("Credit")]
    public IActionResult Credit()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("_SESSIONID")))
            return RedirectToAction("Login", "Compte");

        return View();
    }

    [HttpGet]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Remove("_SESSIONID");
        return RedirectToAction("Login", "Compte");
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(ClientLoginViewModel resource)
    {
        if (!ModelState.IsValid)
            return new JsonResult(new
            { }, ConfigConstant.JsonSettings())
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        try
        {
            var loginResource = new ClientLoginResource()
            {
                Indice = resource.ClientLoginResource.Indice,
                Telephone = resource.ClientLoginResource.Telephone
            };
            var content = new StringContent(JsonConvert.SerializeObject(loginResource), Encoding.UTF8, "application/json");
            var result = await Client.PostAsync(URL + "/Client/login", content);
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<ClientResource>();
                HttpContext.Session.SetString("_SESSIONID", response.Id.ToString());
                TempData["User"] = $"{response.Nom} {response.Prenoms}";
                return new JsonResult(new
                {
                    title = "Connexion",
                    typeMessage = TypeMessage.Success.GetString(),
                    message = "Connexion effectuée avec succès",
                    description = string.Empty,
                    timeOut = 8000,
                    strJsonLogin = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<ClientResource>(
                       await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                }, ConfigConstant.JsonSettings())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

            }
            var jQueryViewModel = await AppConstant.GetResponseMessage(result);
            return new JsonResult(new
            {
                title = "Connexion",
                typeMessage = TypeMessage.Error.GetString(),
                message = "Identifiants incorrects",
                description = "Vos identifiants sont incorrects",
                strJsonLogin = JsonConvert.SerializeObject(jQueryViewModel),
                timeOut = jQueryViewModel.TimeOut

            });
        }

        catch (Exception e)
        {

            return new JsonResult(new
            {
                title = "Erreur",
                typeMessage = TypeMessage.Error.GetString(),
                message = e.Message,
                description = e.Message,
                timeOut = 8000
            }, ConfigConstant.JsonSettings())
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }

    [HttpPost]
    [Route("DemandeCredit")]
    public async Task<IActionResult> DemandeCredit(SaveDossierClientResourceViewModel model)
    {
        if (ModelState.HasReachedMaxErrors)
            return new JsonResult(new
            { }, ConfigConstant.JsonSettings())
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        try
        {
            var contentData = new MultipartFormDataContent();
            var ressource = new DossierClientResource()
            {
                Taille = model.SaveDossierClientResource.Taille,
                Poids = model.SaveDossierClientResource.Poids,
                TensionArterielle = model.SaveDossierClientResource.TensionArterielle,
                Fumeur = model.SaveDossierClientResource.Fumeur,
                NbrCigarettes = model.SaveDossierClientResource.NbrCigarettes,
                Buveur = model.SaveDossierClientResource.Buveur,
                Distractions = model.SaveDossierClientResource.Distractions,
                EcheanceCarteIdentite = model.SaveDossierClientResource.EcheanceCarteIdentite,
                EstSportif = model.SaveDossierClientResource.EstSportif,
                CategorieSport = model.SaveDossierClientResource.CategorieSport,
                EstInfirme = model.SaveDossierClientResource.EstInfirme,
                NatureInfirmite = model.SaveDossierClientResource.NatureInfirmite,
                DateSurvenance = model.SaveDossierClientResource.DateSurvenance,
                DateSoumission = DateTime.Now.ToString("dd/MM/yyyy"),
                StatutMaritalId = model.SaveDossierClientResource.StatutMaritalId,
                ClientId = int.Parse(HttpContext.Session.GetString("_SESSIONID")),
                Montant = model.SaveDossierClientResource.Montant
            };
            var client = TempData["User"].ToString();
            var clientFolder = $"\\{client}";
            if (!model.SaveDossierClientResource.ContratTravail.IsNull())
            {
                ressource.ContratTravail = await UploadPdfFile(_webHostEnvironment, model.SaveDossierClientResource.ContratTravail, GlobalConstants.DossierCredit + clientFolder, client, 2);
            }
            if (!model.SaveDossierClientResource.AttestationTravail.IsNull())
            {
                ressource.AttestationTravail = await UploadPdfFile(_webHostEnvironment, model.SaveDossierClientResource.AttestationTravail, GlobalConstants.DossierCredit + clientFolder, client, 1);
            }
            if (!model.SaveDossierClientResource.PremierBulletinSalaire.IsNull())
            {
                ressource.PremierBulletinSalaire = await UploadPdfFile(_webHostEnvironment, model.SaveDossierClientResource.PremierBulletinSalaire, GlobalConstants.DossierCredit + clientFolder, client, 3);
            }
            if (!model.SaveDossierClientResource.DeuxiemeBulletinSalaire.IsNull())
            {
                ressource.DeuxiemeBulletinSalaire = await UploadPdfFile(_webHostEnvironment, model.SaveDossierClientResource.DeuxiemeBulletinSalaire, GlobalConstants.DossierCredit + clientFolder, client, 4);
            }
            if (!model.SaveDossierClientResource.TroisiemeBulletinSalaire.IsNull())
            {
                ressource.TroisiemeBulletinSalaire = await UploadPdfFile(_webHostEnvironment, model.SaveDossierClientResource.TroisiemeBulletinSalaire, GlobalConstants.DossierCredit + clientFolder, client, 5);
            }
            if (!model.SaveDossierClientResource.FactureProFormat.IsNull())
            {
                ressource.FactureProFormat = await UploadPdfFile(_webHostEnvironment, model.SaveDossierClientResource.FactureProFormat, GlobalConstants.DossierCredit + clientFolder, client, 6);
            }
            if (!model.SaveDossierClientResource.CarteIdentite.IsNull())
            {
                ressource.CarteIdentite = await UploadPdfFile(_webHostEnvironment, model.SaveDossierClientResource.FactureProFormat, GlobalConstants.DossierCredit + clientFolder, client, 7);
            }
            var content = new StringContent(JsonConvert.SerializeObject(ressource), Encoding.UTF8, "application/json");
               
            Client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            var result = await Client.PostAsync(URL + "/DossierClient/add", content);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<DossierClientResource>();
                TempData["Credit"] = response.Id;
                return new JsonResult(new
                {
                    title = "Demande de crédit",
                    typeMessage = TypeMessage.Success.GetString(),
                    message = "Demande de crédit effectuée avec succès",
                    description = string.Empty,
                    timeOut = 8000,
                    strJsonDemandeCredit = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<DossierClientResource>(
                       await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                }, ConfigConstant.JsonSettings())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            var jQueryViewModel = await AppConstant.GetResponseMessage(result);
            return new JsonResult(new
            {
                title = "Demande de crédit",
                typeMessage = TypeMessage.Error.GetString(),
                message = "Une erreur est survenue",
                description = "Vos informations sont incorrectes, réessayez à nouveau",
                strJsonDemandeCredit = JsonConvert.SerializeObject(jQueryViewModel),
                timeOut = jQueryViewModel.TimeOut

            });
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                title = "Erreur",
                typeMessage = TypeMessage.Error.GetString(),
                message = ex.Message,
                description = ex.Message,
                timeOut = 8000
            }, ConfigConstant.JsonSettings())
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }

    [Route("erreur")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        ViewBag.Title = "Erreur";

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [Route("page-bientôt-disponible")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult ComingSoon()
    {
        ViewBag.Title = "Bientôt disponible";

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Route("page-non-disponible")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult PageNotAvailable()
    {
        ViewBag.Title = "Bientôt disponible";

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [Route("page-non-autorisée")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult NotAuthorized()
    {
        ViewBag.Title = "Page non autorisée";

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [Route("page-en-maintenance")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult UnderMaintenance()
    {
        ViewBag.Title = "Maintenance";

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public static MultipartFormDataContent Data(MultipartFormDataContent content, IFormFile file, string field)
    {
        byte[] data;
        using (var br = new BinaryReader(file.OpenReadStream()))
        {
            data = br.ReadBytes((int)file.OpenReadStream().Length);
        }
        var bytes = new ByteArrayContent(data);
        content.Add(bytes, field, file.FileName);

        return content;
    }

    public static byte[]? Data(IFormFile file)
    {
        if(file is not null)
        {
            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
            {
                data = br.ReadBytes((int)file.OpenReadStream().Length);
            }

            return data;
        }

        return null;
    }


    public static async Task<string?> UploadPdfFile(IHostEnvironment env, IFormFile? file, string path, string nomClient, int i)
    {
        if (file is not { Length: > 0 }) return null;

        string nomFichier = "";
        try
        {
            var extension = Path.GetExtension(file.FileName);
            if (!extension.ToLower().In(".pdf"))
                return "NotAccepted";
            switch (i)
            {
                case 1: //Attestation de travail
                    nomFichier = $"{nomClient}_ATTESTATION_TRAVAIL" + extension;
                    break;
                case 2: //CONTRAT TRAVAIL
                    nomFichier = $"{nomClient}_CONTRAT_TRAVAIL" + extension;
                    break;
                case 3: //PREMIER_BULLETIN_SALAIRE
                    nomFichier = $"{nomClient}_PREMIER_BULLETIN_SALAIRE" + extension;
                    break;
                case 4: //DEUXIEME BULLETIN SALAIRE
                    nomFichier = $"{nomClient}_DEUXIEME_BULLETIN_SALAIRE" + extension;
                    break;
                case 5: //TROISIEME_BULLETIN_SALAIRE
                    nomFichier = $"{nomClient}_TROISIEME_BULLETIN_SALAIRE" + extension;
                    break;
                case 6: //FACTURE PROFORMAT
                    nomFichier = $"{nomClient}_FACTURE_PROFORMA" + extension;
                    break;
                case 7: //CARTE_IDENTITE
                    nomFichier = $"{nomClient}_CARTE_IDENTITE" + extension;
                    break;

                default:
                    break;
            }
            //nomFichier = UtilsConstant.RandomString(6) + DateTime.Now.ToString("yyyymmddssfff") + extension;
            var repertoireServeur = Path.Combine(env.ContentRootPath, path);
            if (!Directory.Exists(repertoireServeur)) Directory.CreateDirectory(repertoireServeur);
            using FileStream fileStream = new(Path.Combine(repertoireServeur, nomFichier), FileMode.OpenOrCreate);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return nomFichier;
    }

}
