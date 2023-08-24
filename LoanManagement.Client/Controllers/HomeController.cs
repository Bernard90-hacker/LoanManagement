using LoanManagement.Client.Resources;

namespace FileProcess.Fps.MVC.Controllers;

/// <summary>
/// 
/// </summary>
[Route("")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _config;
    private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
    private readonly IMemoryCache _memoryCache;
    private readonly IDataProtector _protector;
    private readonly HttpClient Client = new HttpClient();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="config"></param>
    /// <param name="sharedLocalizer"></param>
    /// <param name="memoryCache"></param>
    /// <param name="protector"></param>
    public HomeController(ILogger<HomeController> logger,
        IConfiguration config, IStringLocalizer<SharedResource> sharedLocalizer,
        IMemoryCache memoryCache, IDataProtectionProvider protectionProvider,
        DataProtectionPurposeStrings dataProtectionPurposeStrings)
    {
        _logger = logger;
        _config = config;
        _sharedLocalizer = sharedLocalizer;
        _memoryCache = memoryCache;
        _protector = protectionProvider.CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
    }

    /// <summary>
    /// 
    /// </summary>
    private static string URL = MyConstants.LoanManagementApiUrl;
    /// <summary>
    /// 
    /// </summary>
    private static string AuthentificationString => Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"Admin:@dmin*Fps*2022;"));

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Index()
    {
        ConfigConstant.AddCurrentRouteToSession(HttpContext);
        if (HttpContext.Session.GetString("_SESSIONID").IsNull())
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpGet]
    [Route("MesDemandes")]
    public async Task<ActionResult> MesDemandes()
    {
        ConfigConstant.AddCurrentRouteToSession(HttpContext);
        try
        {
            if (HttpContext.Session.GetString("_SESSIONID").IsNull())
                return RedirectToAction("Index", "Home");
            var id = int.Parse(HttpContext.Session.GetString("_SESSIONID"));
            var result = await Client.GetAsync(URL + $"/DossierClient/Client/{id}");
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<SaveDossierClientResource>();
                ViewBag.Response = response;
            }
            return View();  
        }
        catch (Exception)
        {

            return View();
        }
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(ClientLoginViewModel model)
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
                Indice = model.ClientLoginResource.Indice, 
                Telephone = model.ClientLoginResource.Telephone
			};
            var content = new StringContent(JsonConvert.SerializeObject(loginResource), Encoding.UTF8, "application/json");
            var result = await Client.PostAsync(URL + "/Client/login", content);
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<ClientResource>();
                HttpContext.Session.SetString("_SESSIONID", response.Id.ToString());
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



    /// <summary>
    /// 
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    //[HttpPost]
    //[Route("remise-odre")]
    //public async Task<IActionResult> RemiseOrdre(List<IFormFile> files, string dateTraitement)
    //{
    //    try
    //    {
    //        var contentData = new MultipartFormDataContent();
    //        foreach (var file in files)
    //        {
    //            byte[] data;
    //            using (var br = new BinaryReader(file.OpenReadStream()))
    //            {
    //                data = br.ReadBytes((int)file.OpenReadStream().Length);
    //            }
    //            var bytes = new ByteArrayContent(data);
    //            contentData.Add(bytes, "files", file.FileName);
    //        }
    //        contentData.Add(new StringContent(dateTraitement), "dateTraitement");

    //        using var httpClient = new HttpClient();
    //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthentificationString);
    //        httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
    //        using var response = await httpClient.PostAsync(UrlFpsBase + "FileProcessing/UploadGSProcessFiles", contentData);
    //        var result = response.IsSuccessStatusCode;
    //        if (result)
    //        {
    //            if (TempData.ContainsKey("chargementVirements"))
    //                TempData.Remove("chargementVirements");
    //            var content = await response.Content.ReadAsStringAsync();
    //            TempData["chargementVirements"] = content;
    //            var chargementVirements = JsonConvert.DeserializeObject<List<ChargementVirementViewModel>>(content, ConfigConstant.SetDateTimeConverter());

    //            return new JsonResult(new
    //            {
    //                title = _sharedLocalizer["Success"].ToString(),
    //                typeMessage = TypeMessage.Success.GetString(),
    //                message = _sharedLocalizer["SuccèsDeRemiseOrdre"].ToString(),
    //                description = string.Empty,
    //                timeOut = 8000,
    //                culture = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name ?? "fr",
    //                chargementVirements
    //            }, ConfigConstant.JsonSettings())
    //            {
    //                StatusCode = (int)HttpStatusCode.OK
    //            };
    //        }

    //        var jQueryViewModel = await AppConstant.GetResponseMessage(response, _sharedLocalizer);
    //        return new JsonResult(new
    //        {
    //            title = jQueryViewModel.Title,
    //            typeMessage = jQueryViewModel.TypeMessage,
    //            message = jQueryViewModel.Message,
    //            description = jQueryViewModel.Description,
    //            erreurs = jQueryViewModel.Errors,
    //            timeOut = jQueryViewModel.TimeOut
    //        }, ConfigConstant.JsonSettings())
    //        {
    //            StatusCode = (int)HttpStatusCode.BadRequest
    //        };
    //    }
    //    catch (Exception ex)
    //    {
    //        return new JsonResult(new
    //        {
    //            title = _sharedLocalizer["Erreur"].ToString(),
    //            typeMessage = TypeMessage.Error.GetString(),
    //            message = _sharedLocalizer["ErreurProduite"].ToString(),
    //            description = ex.Message,
    //            timeOut = 8000
    //        }, ConfigConstant.JsonSettings())
    //        {
    //            StatusCode = (int)HttpStatusCode.InternalServerError
    //        };
    //    }
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    //[HttpGet]
    //[Route("paramétrage-variables")]
    //public async Task<IActionResult> Parametrage()
    //{
    //    ConfigConstant.AddCurrentRouteToSession(HttpContext);

    //    using var httpClient = new HttpClient();
    //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthentificationString);
    //    using var response = await httpClient.GetAsync(UrlFpsBase + nameof(ParamGlobal));
    //    var paramGlobal = JsonConvert.DeserializeObject<ParamGlobal>(await response.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter());

    //    var paramGlobalViewModel = new ParamGlobalViewModel
    //    {
    //        ParamGlobal = new ParamGlobal
    //        {
    //            TailleNumeroCompte = paramGlobal.TailleNumeroCompte ?? 0,
    //            TailleCleRib = paramGlobal.TailleCleRib ?? 0,
    //            TailleCodeBanque = paramGlobal.TailleCodeBanque ?? 0,
    //            TailleCodeAgence = paramGlobal.TailleCodeAgence ?? 0,
    //            FormatAccentuation = paramGlobal.FormatAccentuation,
    //            FormatPonctuation = paramGlobal.FormatPonctuation,
    //            SignesPonctuation = !paramGlobal.SignesPonctuation.Contains("\"\"\"") ? paramGlobal.SignesPonctuation : paramGlobal.SignesPonctuation.Replace("\"\"\"", '"' + "\\\"" + '"'),
    //            CodeBanqueVirement = paramGlobal.CodeBanqueVirement,
    //            TailleCodeBanqueVirement = paramGlobal.TailleCodeBanqueVirement,
    //            CodeAgenceVirement = paramGlobal.CodeAgenceVirement,
    //            TailleCodeAgenceVirement = paramGlobal.TailleCodeAgenceVirement,
    //            NumeroCompteVirement = paramGlobal.NumeroCompteVirement,
    //            TailleNumeroCompteVirement = paramGlobal.TailleNumeroCompteVirement,
    //            CleRibVirement = paramGlobal.CleRibVirement,
    //            TailleCleRibVirement = paramGlobal.TailleCleRibVirement,
    //            MotifVirement = paramGlobal.MotifVirement
    //        }
    //    };

    //    return View(paramGlobalViewModel);
    //}
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="paramGlobalViewModel"></param>
    /// <returns></returns>
    //[HttpPost]
    //[Route("paramétrage-variables/édition")]
    //public async Task<IActionResult> EditParamGlobal(ParamGlobalViewModel paramGlobalViewModel)
    //{
    //    if (!ModelState.IsValid)
    //        return new JsonResult(new
    //        { }, ConfigConstant.JsonSettings())
    //        {
    //            StatusCode = (int)HttpStatusCode.BadRequest
    //        };

    //    try
    //    {
    //        var builder = new StringBuilder();
    //        if (!paramGlobalViewModel.ParamGlobal.SignesPonctuation.IsNullOrEmpty())
    //        {
    //            foreach (var s in JsonConvert.DeserializeObject<List<SignesPonctuation>>(paramGlobalViewModel.ParamGlobal.SignesPonctuation))
    //            {
    //                if (!s.Value.IsNullOrEmpty())
    //                    builder.Append("\"" + s.Value + "\"").Append(",");
    //            }
    //        }
    //        var paramGlobal = new ParamGlobal()
    //        {
    //            TailleNumeroCompte = paramGlobalViewModel.ParamGlobal.TailleNumeroCompte,
    //            TailleCleRib = paramGlobalViewModel.ParamGlobal.TailleCleRib,
    //            TailleCodeBanque = paramGlobalViewModel.ParamGlobal.TailleCodeBanque,
    //            TailleCodeAgence = paramGlobalViewModel.ParamGlobal.TailleCodeAgence,
    //            FormatAccentuation = paramGlobalViewModel.ParamGlobal.FormatAccentuation,
    //            FormatPonctuation = paramGlobalViewModel.ParamGlobal.FormatPonctuation,
    //            SignesPonctuation = builder.ToString().TrimEnd(new char[] { ',' }),
    //            CodeBanqueVirement = paramGlobalViewModel.ParamGlobal.CodeBanqueVirement,
    //            CodeAgenceVirement = paramGlobalViewModel.ParamGlobal.CodeAgenceVirement,
    //            NumeroCompteVirement = paramGlobalViewModel.ParamGlobal.NumeroCompteVirement,
    //            CleRibVirement = paramGlobalViewModel.ParamGlobal.CleRibVirement,
    //            MotifVirement = paramGlobalViewModel.ParamGlobal.MotifVirement
    //        };
    //        var contentData = new StringContent(JsonConvert.SerializeObject(paramGlobal), Encoding.UTF8, "application/json");
    //        using var httpClient = new HttpClient();
    //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthentificationString);
    //        using var response = await httpClient.PutAsync(UrlFpsBase + nameof(ParamGlobal), contentData);
    //        var result = response.IsSuccessStatusCode;
    //        if (result)
    //            return new JsonResult(new
    //            {
    //                title = _sharedLocalizer["Succès"].ToString(),
    //                typeMessage = TypeMessage.Success.GetString(),
    //                message = _sharedLocalizer["SuccèsMajParamGlobal"].ToString(),
    //                description = string.Empty,
    //                timeOut = 8000,
    //                strJsonParamGlobal = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<ParamGlobal>(
    //                    await response.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
    //            }, ConfigConstant.JsonSettings())
    //            {
    //                StatusCode = (int)HttpStatusCode.OK
    //            };

    //        var jQueryViewModel = await AppConstant.GetResponseMessage(response, _sharedLocalizer);
    //        return new JsonResult(new
    //        {
    //            title = jQueryViewModel.Title,
    //            typeMessage = jQueryViewModel.TypeMessage,
    //            message = jQueryViewModel.Message,
    //            description = jQueryViewModel.Description,
    //            erreurs = jQueryViewModel.Errors,
    //            timeOut = jQueryViewModel.TimeOut
    //        }, ConfigConstant.JsonSettings())
    //        {
    //            StatusCode = (int)HttpStatusCode.BadRequest
    //        };
    //    }
    //    catch (Exception ex)
    //    {
    //        return new JsonResult(new
    //        {
    //            title = _sharedLocalizer["Erreur"].ToString(),
    //            typeMessage = TypeMessage.Error.GetString(),
    //            message = _sharedLocalizer["ErreurProduite"].ToString(),
    //            description = ex.Message,
    //            timeOut = 8000
    //        }, ConfigConstant.JsonSettings())
    //        {
    //            StatusCode = (int)HttpStatusCode.InternalServerError
    //        };
    //    }
    //}
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="paramGlobalViewModel"></param>
    /// <returns></returns>
    //[HttpPost]
    //[Route("paramétrage-infos-virement/édition")]
    //public async Task<IActionResult> EditInfosVirement(ParamGlobalViewModel paramGlobalViewModel)
    //{
    //    try
    //    {
    //        var paramInfosVirement = new ParamInfosVirement()
    //        {
    //            TailleNumeroCompteVirement = paramGlobalViewModel.ParamGlobal.TailleNumeroCompteVirement,
    //            TailleCleRibVirement = paramGlobalViewModel.ParamGlobal.TailleCleRibVirement,
    //            TailleCodeBanqueVirement = paramGlobalViewModel.ParamGlobal.TailleCodeBanqueVirement,
    //            TailleCodeAgenceVirement = paramGlobalViewModel.ParamGlobal.TailleCodeAgenceVirement
    //        };
    //        var contentData = new StringContent(JsonConvert.SerializeObject(paramInfosVirement), Encoding.UTF8, "application/json");
    //        using var httpClient = new HttpClient();
    //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthentificationString);
    //        using var response = await httpClient.PutAsync(UrlFpsBase + nameof(ParamGlobal) + "/UpdateParamInfoVirement", contentData);
    //        var result = response.IsSuccessStatusCode;
    //        if (result)
    //            return new JsonResult(new
    //            {
    //                title = _sharedLocalizer["Succès"].ToString(),
    //                typeMessage = TypeMessage.Success.GetString(),
    //                message = _sharedLocalizer["SuccèsMajParamInfosVirement"].ToString(),
    //                description = string.Empty,
    //                timeOut = 8000,
    //                strJsonParamGlobal = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<ParamGlobal>(
    //                    await response.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
    //            }, ConfigConstant.JsonSettings())
    //            {
    //                StatusCode = (int)HttpStatusCode.OK
    //            };

    //        var jQueryViewModel = await AppConstant.GetResponseMessage(response, _sharedLocalizer);
    //        return new JsonResult(new
    //        {
    //            title = jQueryViewModel.Title,
    //            typeMessage = jQueryViewModel.TypeMessage,
    //            message = jQueryViewModel.Message,
    //            description = jQueryViewModel.Description,
    //            erreurs = jQueryViewModel.Errors,
    //            timeOut = jQueryViewModel.TimeOut
    //        }, ConfigConstant.JsonSettings())
    //        {
    //            StatusCode = (int)HttpStatusCode.BadRequest
    //        };
    //    }
    //    catch (Exception ex)
    //    {
    //        return new JsonResult(new
    //        {
    //            title = _sharedLocalizer["Erreur"].ToString(),
    //            typeMessage = TypeMessage.Error.GetString(),
    //            message = _sharedLocalizer["ErreurProduite"].ToString(),
    //            description = ex.Message,
    //            timeOut = 8000
    //        }, ConfigConstant.JsonSettings())
    //        {
    //            StatusCode = (int)HttpStatusCode.InternalServerError
    //        };
    //    }
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //[HttpGet]
    //[Route("download-file")]
    //public ActionResult DownloadFile(string id)
    //{
    //    var chargementVirementViewModels = TempData.ContainsKey("chargementVirements") ? JsonConvert.DeserializeObject
    //        <List<ChargementVirementViewModel>>((string)TempData["chargementVirements"] ?? string.Empty,
    //        ConfigConstant.SetDateTimeConverter()) : null;
    //    TempData.Keep("chargementVirements");
    //    if (!chargementVirementViewModels.IsNull())
    //    {
    //        var chargementVirement = chargementVirementViewModels.SingleOrDefault(c => id.Equals(c.Id.ToString()));

    //        return File(System.IO.File.ReadAllBytes(chargementVirement.CheminAcces), "text/plain", Path.GetFileName(chargementVirement.CheminAcces));
    //    }

    //    return NoContent();
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    //[HttpGet]
    //[Route("download-files")]
    //public ActionResult DownloadFiles()
    //{
    //    var chargementVirementViewModels = TempData.ContainsKey("chargementVirements") ? JsonConvert.DeserializeObject
    //        <List<ChargementVirementViewModel>>((string)TempData["chargementVirements"] ?? string.Empty,
    //        ConfigConstant.SetDateTimeConverter()) : null;
    //    TempData.Keep("chargementVirements");
    //    if (!chargementVirementViewModels.IsNull())
    //    {
    //        if (chargementVirementViewModels.Count == 1)
    //        {
    //            var chargementVirement = chargementVirementViewModels.SingleOrDefault();

    //            return File(System.IO.File.ReadAllBytes(chargementVirement.CheminAcces), "text/plain", Path.GetFileName(chargementVirement.CheminAcces));
    //        }

    //        List<string> filePaths = new();
    //        foreach (var chargementVirementViewModel in chargementVirementViewModels)
    //            filePaths.Add(chargementVirementViewModel.CheminAcces);

    //        var (fileType, archiveData, archiveName) = UtilsConstant.DownloadFiles($"Archive-VIREMENT_SAL-{DateTime.Now:yyMMddHHmmss}.zip", filePaths);

    //        return File(archiveData, fileType, archiveName);
    //    }

    //    return NoContent();
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Route("erreur")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        ViewBag.Title = _sharedLocalizer["Erreur"].ToString();

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Route("page-bientôt-disponible")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult ComingSoon()
    {
        ViewBag.Title = _sharedLocalizer["BientôtDisponible"].ToString();

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Route("page-non-disponible")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult PageNotAvailable()
    {
        ViewBag.Title = _sharedLocalizer["BientôtDisponible"].ToString();

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Route("page-non-autorisée")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult NotAuthorized()
    {
        ViewBag.Title = _sharedLocalizer["PageNonAutorisée"].ToString();

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Route("page-en-maintenance")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult UnderMaintenance()
    {
        ViewBag.Title = _sharedLocalizer["Maintenance"].ToString();

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
