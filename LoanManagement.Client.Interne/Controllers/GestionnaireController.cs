using System.Reflection;

namespace LoanManagement.Client.Interne.Controllers
{
    [Route("")]
    public class GestionnaireController : Controller
    {
        private readonly ILogger<GestionnaireController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient httpClient = new HttpClient();

        private static string URL = MyConstants.LoanManagementApiUrl;
        private static string URL1 = MyConstants.GduApiUrl;

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
        [Route("Gestionnaire/DossierCredit/{id}")]
        public async Task<IActionResult> DossierCredit(int id)
        {
            var dossiers = await httpClient.GetAsync(URL + $"/DossierClient/{id}");
            var employeurs = await httpClient.GetAsync(URL + "/Employeur/all");
            if (dossiers.IsSuccessStatusCode && employeurs.IsSuccessStatusCode)
            {
                ViewBag.Dossiers = await dossiers.Content.ReadFromJsonAsync<GetDossierClientResource>();
                ViewBag.Employeurs = await employeurs.Content.ReadFromJsonAsync<IEnumerable<EmployeurRessource>>();
            }
            return View();
        }

		[HttpGet]
		[Route("UpdatePassword")]
		public async Task<IActionResult> UpdatePassword()
		{
			return View();
		}

		[HttpPost]
		[Route("UpdatePasswordAction")]
		public async Task<IActionResult> UpdatePasswordAction(UserUpdateViewModel model)
		{
			if (ModelState.HasReachedMaxErrors)
			{
				return new JsonResult(new
				{ }, ConfigConstant.JsonSettings())
				{
					StatusCode = (int)HttpStatusCode.BadRequest
				};
			}
			try
			{

				var resource = new UserUpdateResource()
				{
					Username = model.Resource.Username,
					OldPassword = model.Resource.OldPassword,
					NewPassword = model.Resource.NewPassword,
					ConfirmPassword = model.Resource.ConfirmPassword

				};
				var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
				var result = await httpClient.PatchAsync(URL1 + "/Utilisateur/updatePassword", content);
				if (result.IsSuccessStatusCode)
				{
					var response = await result.Content.ReadFromJsonAsync<GetUtilisateurResource>();

					return new JsonResult(new
					{
						title = "Modification du mot de passe",
						typeMessage = TypeMessage.Success.GetString(),
						message = "Opération effectuée avec succès",
						description = string.Empty,
						timeOut = 8000,
						strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<GetUtilisateurResource>(
						   await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
					}, ConfigConstant.JsonSettings())
					{
						StatusCode = (int)HttpStatusCode.OK
					};

				}
				var jQueryViewModel = await AppConstant.GetResponseMessage(result);
				return new JsonResult(new
				{
					title = "Modification du mot de passe",
					typeMessage = TypeMessage.Error.GetString(),
					message = "Erreur",
					description = "Une erreur est survenue pendant le traitement de la requête.",
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
        [Route("MonterDossierCredit")]
        public async Task<IActionResult> MonterDossierCredit(PretAccordViewModel model)
        {
            if (ModelState.HasReachedMaxErrors)
            {
                return new JsonResult(new
                { }, ConfigConstant.JsonSettings())
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            try
            {
                var dossier = ViewBag.Dossiers as GetDossierClientResource;
               
                var resource = new PretAccordResource()
                {
                    MontantPret = model.PretAccordResource.MontantPret,
                    DatePremiereEcheance = model.PretAccordResource.DatePremiereEcheance,
                    DateDerniereEcheance = model.PretAccordResource.DateDerniereEcheance,
                    MontantPrime = model.PretAccordResource.MontantPrime,
                    Surprime = model.PretAccordResource.Surprime,
                    SalaireNetMensuel = model.PretAccordResource.SalaireNetMensuel,
                    Mensualite = model.PretAccordResource.Mensualite,
                    DateDepartRetraite = model.PretAccordResource.DateDepartRetraite,
                    TypePretId = model.PretAccordResource.TypePretId,
                    PeriodicitePaiementId = model.PretAccordResource.PeriodicitePaiementId,
                    DossierClientId = model.PretAccordResource.DossierClientId,
                    EmployeurId = model.PretAccordResource.EmployeurId,
                };
                var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(URL + "/PretAccord/add", content);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<PretAccordResource>();

                    return new JsonResult(new
                    {
                        title = "Instruction d'un dossier crédit",
                        typeMessage = TypeMessage.Success.GetString(),
                        message = "Opération effectuée avec succès",
                        description = string.Empty,
                        timeOut = 8000,
                        strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PretAccordResource>(
                           await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                    }, ConfigConstant.JsonSettings())
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };

                }
                var jQueryViewModel = await AppConstant.GetResponseMessage(result);
                return new JsonResult(new
                {
                    title = "Instruction d'un dossier crédit",
                    typeMessage = TypeMessage.Error.GetString(),
                    message = "Erreur",
                    description = "Une erreur est survenue pendant le traitement de la requête.",
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

        [HttpGet]
        [Route("Employeur")]
        public IActionResult Employeur()
        {

            return View();
        }

		[HttpPost]
		[Route("AddEmployeur")]
		public async Task<IActionResult> AddEmployeur(EmployeurViewModel model)
		{
			if (ModelState.HasReachedMaxErrors)
			{
				return new JsonResult(new
				{ }, ConfigConstant.JsonSettings())
				{
					StatusCode = (int)HttpStatusCode.BadRequest
				};
			}
			try
			{

				var resource = new EmployeurRessource()
				{
					Nom = model.EmployeurResource.Nom,
					Tel = model.EmployeurResource.Tel,
					BoitePostale = model.EmployeurResource.BoitePostale
					
				};
				var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
				var result = await httpClient.PostAsync(URL + "/Employeur/add", content);
				if (result.IsSuccessStatusCode)
				{
					var response = await result.Content.ReadFromJsonAsync<EmployeurRessource>();

					return new JsonResult(new
					{
						title = "Ajout d'un employeur",
						typeMessage = TypeMessage.Success.GetString(),
						message = "Opération effectuée avec succès",
						description = string.Empty,
						timeOut = 8000,
						strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<EmployeurRessource>(
						   await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
					}, ConfigConstant.JsonSettings())
					{
						StatusCode = (int)HttpStatusCode.OK
					};

				}
				var jQueryViewModel = await AppConstant.GetResponseMessage(result);
				return new JsonResult(new
				{
					title = "Ajout d'un employeur",
					typeMessage = TypeMessage.Error.GetString(),
					message = "Erreur",
					description = "Une erreur est survenue pendant le traitement de la requête.",
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

		[HttpGet]
        [Route("ListeDossierCredit")]
        public IActionResult ListeDossierCredit()
        {

            return View();
        }

        [HttpGet]
        [Route("ListeDossiersInstruits")]
        public IActionResult ListeDossiersInstruits()
        {

            return View();
        }

        [Route("Demandes")]
		public async Task<IActionResult> Demandes()
		{
			
			var demandes = await httpClient.GetAsync(URL + $"/DossierClient/dossierNonMontes/");
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
		[Route("Dossiers")]
		public async Task<IActionResult> Dossiers()
		{

			var demandes = await httpClient.GetAsync(URL + $"/PretAccord/all/");
			if (demandes.IsSuccessStatusCode)
			{
				return new JsonResult(new
				{
					title = "Dossiers",
					typeMessage = TypeMessage.Success.GetString(),
					message = "Liste des demandes",
					description = string.Empty,
					timeOut = 8000,
					strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<IEnumerable<PretAccordResource>>(
						  await demandes.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
				}, ConfigConstant.JsonSettings())
				{
					StatusCode = (int)HttpStatusCode.OK
				};
			}
			var jQueryViewModel = await AppConstant.GetResponseMessage(demandes);
			return new JsonResult(new
			{
				title = "Liste des dossiers",
				typeMessage = TypeMessage.Error.GetString(),
				message = "Une erreur s'est produite",
				description = "Vos identifiants sont incorrects",
				strJsonLogin = JsonConvert.SerializeObject(jQueryViewModel),
				timeOut = jQueryViewModel.TimeOut

			});
		}


		[HttpGet]
		[Route("Gestionnaire/Details/{id}")]
		public async Task<IActionResult> Details(int id)
        {
            var demande = await httpClient.GetAsync(URL + $"/DossierClient/{id}");
            if (demande.IsSuccessStatusCode)
            {
                var x = await demande.Content.ReadFromJsonAsync<GetDossierClientResource>();
                ViewBag.Demande = x;
                var client = await httpClient.GetAsync(URL + $"/Client/id?={x.ClientId}");
                var y = await client.Content.ReadFromJsonAsync<ClientResource>();
                ViewBag.Customer = $"{y.Nom} {y.Prenoms}";
            }

            return View();
        }
        [HttpGet]
        [Route("Gestionnaire/Traitement/{id}")]
        public async Task<IActionResult> Traitement(int id)
        {
			ViewBag.DossierId = id.ToString();

            return View();
        }

		[HttpPost]
		[Route("TraiterDossier")]
		public async Task<IActionResult> TraiterDossier(TraitementViewModel model)
		{
			if (ModelState.HasReachedMaxErrors)
			{
				return new JsonResult(new
				{ }, ConfigConstant.JsonSettings())
				{
					StatusCode = (int)HttpStatusCode.BadRequest
				};
			}
			try
			{

				var resource = new TraitementDossierResource()
				{
					Id = model.Resource.Id,
					Motif = model.Resource.Motif,
					Response = model.Resource.Response

				};
				var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
				var result = await httpClient.PostAsync(URL + "/PretAccord/traiter", content);
				if (result.IsSuccessStatusCode)
				{


					return new JsonResult(new
					{
						title = "Traitement d'un dossier",
						typeMessage = TypeMessage.Success.GetString(),
						message = "Opération effectuée avec succès",
						description = string.Empty,
						timeOut = 8000
					});

				}
				var jQueryViewModel = await AppConstant.GetResponseMessage(result);
				return new JsonResult(new
				{
					title = "Ajout d'un employeur",
					typeMessage = TypeMessage.Error.GetString(),
					message = "Erreur",
					description = "Une erreur est survenue pendant le traitement de la requête."

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
    }
}
