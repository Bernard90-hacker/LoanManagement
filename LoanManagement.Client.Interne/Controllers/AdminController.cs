using System.Reflection;
using System.Security.Policy;

namespace LoanManagement.Client.Interne.Controllers
{
    [Route("")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient httpClient = new HttpClient();

        public AdminController(ILogger<AdminController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        private static string URL = MyConstants.GduApiUrl;
        private static string URL1 = MyConstants.LoanManagementApiUrl;

        [HttpGet]
        [Route("Admin")]
        public async Task<IActionResult> Admin()
        {
            var clients = await httpClient.GetAsync(URL1 + "/Client/all");
            var users = await httpClient.GetAsync(URL + "/Utilisateur/all");
            if(clients.IsSuccessStatusCode && users.IsSuccessStatusCode)
            {
                var x = await clients.Content.ReadFromJsonAsync<IEnumerable<ClientResource>>();
                var y = await users.Content.ReadFromJsonAsync<IEnumerable<Utilisateur>>();
                
                ViewBag.NbrUsers = y.Count().ToString();
                ViewBag.NbrClients = x.Count().ToString();
            }
            ConfigConstant.AddCurrentRouteToSession(HttpContext);
            return View();
        }

        [HttpGet]
        [Route("AddClient")]
        public async Task<IActionResult> AddClient()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);
            var result = await httpClient.GetAsync(URL1 + "/Client/all");
            if(result is not null)
            {
                ViewBag.Clients = await result.Content.ReadFromJsonAsync<IEnumerable<ClientResource>>();
            }
            return View();
        }
        [HttpGet]
        [Route("AddUser")]
        public IActionResult AddUser()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);

            return View();
        }

        [HttpGet]
        [Route("AllCustomers")]
        public async Task<IActionResult> AllCustomers()
        {
            var customers = await httpClient.GetAsync(URL1 + $"/Client/all/");
            if (customers.IsSuccessStatusCode)
            {
                return new JsonResult(new
                {
                    title = "Liste des clients",
                    typeMessage = TypeMessage.Success.GetString(),
                    message = "Ok",
                    description = string.Empty,
                    timeOut = 8000,
                    strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<IEnumerable<ClientResource>>(
                          await customers.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                }, ConfigConstant.JsonSettings())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            var jQueryViewModel = await AppConstant.GetResponseMessage(customers);
            return new JsonResult(new
            {
                title = "Liste des clients",
                typeMessage = TypeMessage.Error.GetString(),
                message = "Erreur",
                description = "Une erreur s'est produite",
                strJsonLogin = JsonConvert.SerializeObject(jQueryViewModel),
                timeOut = jQueryViewModel.TimeOut

            });
        }
        [HttpGet]
        [Route("AllUsers")]
        public async Task<IActionResult> AllUsers()
        {
            var users = await httpClient.GetAsync(URL + $"/Utilisateur/all/");
            if (users.IsSuccessStatusCode)
            {
                return new JsonResult(new
                {
                    title = "Liste des utilisateurs",
                    typeMessage = TypeMessage.Success.GetString(),
                    message = "Ok",
                    description = string.Empty,
                    timeOut = 8000,
                    strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<IEnumerable<GetUtilisateurResource>>(
                          await users.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                }, ConfigConstant.JsonSettings())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            var jQueryViewModel = await AppConstant.GetResponseMessage(users);
            return new JsonResult(new
            {
                title = "Liste des utilisateurs",
                typeMessage = TypeMessage.Error.GetString(),
                message = "Erreur",
                description = "Une erreur s'est produite",
                strJsonLogin = JsonConvert.SerializeObject(jQueryViewModel),
                timeOut = jQueryViewModel.TimeOut

            });
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(UtilisateurViewModel model)
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
                var d = model.UtilisateurResource.DateDesactivation;
                var a = model.UtilisateurResource.DateExpirationCompte;
                var e = d.Split("-");
                var k = e[2] + "/" + e[1] + "/" + e[0];
                e = a.Split("-");
				var l = e[2] + "/" + e[1] + "/" + e[0];
				var resource = new SaveUtilisateurResource()
                {
                   Username = model.UtilisateurResource.Username,
                   Email = model.UtilisateurResource.Email,
                   IsEditPassword = true,
                   IsSuperAdmin = false,
                   IsAdmin = false,
                   ProfilId = model.UtilisateurResource.ProfilId,
                };
                resource.DateDesactivation = k;
                resource.DateExpirationCompte = l;
                var m = DateTime.ParseExact(resource.DateDesactivation, "dd/MM/yyyy", CultureInfo.CurrentCulture);
				if (DateTime.Now >= m)
				{
                    model.UtilisateurResource.Statut = 2;
				}
                else
                {
                    model.UtilisateurResource.Statut = 1;
                }
				var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(URL + "/Utilisateur/register", content);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<SaveUtilisateurResource>();

                    return new JsonResult(new
                    {
                        title = "Ajout d'un utilisateur",
                        typeMessage = TypeMessage.Success.GetString(),
                        message = "Opération effectuée avec succès",
                        description = string.Empty,
                        timeOut = 8000,
                        strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<SaveUtilisateurResource>(
                           await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                    }, ConfigConstant.JsonSettings())
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };

                }
                var jQueryViewModel = await AppConstant.GetResponseMessage(result);
                return new JsonResult(new
                {
                    title = "Ajout d'un utilisateur",
                    typeMessage = TypeMessage.Error.GetString(),
                    message = "Erreur",
                    description = "Une erreur s'est produite",
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
        [Route("ListeUtilisateurs")]
        public IActionResult ListeUtilisateurs()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);

            return View();
        }

        [HttpGet]
        [Route("Parametrage")]
        public IActionResult Parametrage()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);

            return View();
        }

        [HttpGet]
        [Route("Deroulement")]
        public IActionResult Deroulement()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);

            return View();
        }

        [HttpPost]
        [Route("AddDeroulement")]
        public async Task<IActionResult> AddDeroulement(DeroulementViewModel model)
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
                var resource = new DeroulementResource()
                {
                   Plancher = model.Deroulement.Plancher,
                   Plafond = model.Deroulement.Plafond,
                   Libelle = model.Deroulement.Libelle,
                   Description = model.Deroulement.Description,
                   NiveauInstance = model.Deroulement.NiveauInstance,
                   TypePretId = model.Deroulement.TypePretId
                };
                var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(URL1 + "/Deroulement/addDeroulement", content);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<DeroulementResource>();

                    return new JsonResult(new
                    {
                        title = "Configuration d'un déroulement de prêt",
                        typeMessage = TypeMessage.Success.GetString(),
                        message = "Opération effectuée avec succès",
                        description = string.Empty,
                        timeOut = 8000,
                        strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<DeroulementResource>(
                           await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                    }, ConfigConstant.JsonSettings())
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };

                }
                var jQueryViewModel = await AppConstant.GetResponseMessage(result);
                return new JsonResult(new
                {
                    title = "Configuration d'un déroulement de prêt",
                    typeMessage = TypeMessage.Error.GetString(),
                    message = "Erreur",
                    description = "Une erreur s'est produite",
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
        [Route("OrganeDecision")]
        public async Task<IActionResult> OrganeDecision()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);
           

            return View();
        }

		[HttpGet]
		[Route("RoleOrganeDecision")]
		public async Task<IActionResult> RoleOrganeDecision()
		{
			ConfigConstant.AddCurrentRouteToSession(HttpContext);
			var organes = await httpClient.GetAsync(URL1 + "/OrganeDecision/all");
			if (organes.IsSuccessStatusCode)
				ViewBag.organes = await organes.Content.ReadFromJsonAsync<IEnumerable<OrganeDecisionResource>>();

			return View();
		}

		[HttpPost]
		[Route("AddOrganeDecision")]
		public async Task<IActionResult> AddOrganeDecision(OrganeDecisionViewModel model)
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
				var resource = new OrganeDecisionResource()
				{
					Libelle = model.Resource.Libelle
				};
				var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
				var result = await httpClient.PostAsync(URL1 + "/OrganeDecision/add", content);
				if (result.IsSuccessStatusCode)
				{
					var response = await result.Content.ReadFromJsonAsync<OrganeDecisionResource>();

					return new JsonResult(new
					{
						title = "Ajout d'un organe de décision",
						typeMessage = TypeMessage.Success.GetString(),
						message = "Opération effectuée avec succès",
						description = string.Empty,
						timeOut = 8000,
						strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<OrganeDecisionResource>(
						   await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
					}, ConfigConstant.JsonSettings())
					{
						StatusCode = (int)HttpStatusCode.OK
					};

				}
				var jQueryViewModel = await AppConstant.GetResponseMessage(result);
				return new JsonResult(new
				{
					title = "Ajout d'un organe de décision",
					typeMessage = TypeMessage.Error.GetString(),
					message = "Erreur",
					description = "Une erreur s'est produite",
					strJson = JsonConvert.SerializeObject(jQueryViewModel),
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
        [Route("AddRoleOrganeDecision")]
        public async Task<IActionResult> AddRoleOrganeDecision(RoleOrganeDecisionViewModel model)
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
                var resource = new RoleOrganeRessource()
                {
                   Libelle = model.Resource.Libelle,
                   OrganeDecisionId = model.Resource.OrganeDecisionId,
                   DureeTraitement = model.Resource.DureeTraitement
                };
                var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(URL1 + "/Role/add", content);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<RoleOrganeRessource>();

                    return new JsonResult(new
                    {
                        title = "Ajout d'un rôle d'organe",
                        typeMessage = TypeMessage.Success.GetString(),
                        message = "Opération effectuée avec succès",
                        description = string.Empty,
                        timeOut = 8000,
                        strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<RoleOrganeRessource>(
                           await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                    }, ConfigConstant.JsonSettings())
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };

                }
                var jQueryViewModel = await AppConstant.GetResponseMessage(result);
                return new JsonResult(new
                {
                    title = "Ajout d'un rôle d'organe",
                    typeMessage = TypeMessage.Error.GetString(),
                    message = "Erreur",
                    description = "Une erreur s'est produite",
                    strJson = JsonConvert.SerializeObject(jQueryViewModel),
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
        [Route("Etape")]
        public IActionResult Etape()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);

            return View();
        }

        [HttpGet]
        [Route("Compte")]
        public IActionResult Compte()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);

            return View();
        }

        [HttpGet]
        [Route("Employe")]
        public async Task<IActionResult> Employe()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);
            var users = await httpClient.GetAsync(URL + "/Utilisateur/UsersWithoutAccount");
            var departements = await httpClient.GetAsync(URL + "/Departement/all");
            if (users.IsSuccessStatusCode && departements.IsSuccessStatusCode)
            {
                ViewBag.Users = await users.Content.ReadFromJsonAsync<IEnumerable<GetUtilisateurResource>>();
                ViewBag.Departments = await departements.Content.ReadFromJsonAsync<IEnumerable<DepartementResource>>();
            }
            return View();
        }

        [HttpPost]
        [Route("AddEmploye")]
        public async Task<IActionResult> AddEmploye(EmployeViewModel model)
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
                var resource = new EmployeResource()
                {
                    Matricule = model.EmployeResource.Matricule,
                    Nom = model.EmployeResource.Nom,
                    Prenoms = model.EmployeResource.Prenoms,
                    Username = model.EmployeResource.Username,
                    DepartementId = model.EmployeResource.DepartementId
                };
                var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(URL + "/Employe/add", content);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<EmployeResource>();

                    return new JsonResult(new
                    {
                        title = "Ajout d'un employé",
                        typeMessage = TypeMessage.Success.GetString(),
                        message = "Opération effectuée avec succès",
                        description = string.Empty,
                        timeOut = 8000,
                        strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<EmployeResource>(
                           await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                    }, ConfigConstant.JsonSettings())
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };

                }
                var jQueryViewModel = await AppConstant.GetResponseMessage(result);
                return new JsonResult(new
                {
                    title = "Ajout d'un employé",
                    typeMessage = TypeMessage.Error.GetString(),
                    message = "Erreur",
                    description = "Une erreur s'est produite",
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
        [Route("Clients")]
        public async Task<IActionResult> Clients()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);
            var result = await httpClient.GetAsync(URL1 + "/Client/all");
            if (result is not null)
            {
                ViewBag.Clients = await result.Content.ReadFromJsonAsync<IEnumerable<ClientResource>>();
            }
            return View();
        }

        [HttpPost]
        [Route("ParamMotDePasse")]
        public async Task<IActionResult> ParamMotDePasse(ParamMotDePasseViewModel model)
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
                var resource = new ParamMotDePasseResource()
                {
                    IncludeDigits = model.Resource.IncludeDigits,
                    IncludeLowerCase = model.Resource.IncludeLowerCase,
                    IncludeUpperCase = model.Resource.IncludeUpperCase,
                    ExcludeUsername = model.Resource.ExcludeUsername,
                    IncludeSpecialCharacters = model.Resource.IncludeSpecialCharacters,
                    Taille = model.Resource.Taille,
                    DelaiExpiration = model.Resource.DelaiExpiration,
                    Id = 1
                };
                var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
                var result = await httpClient.PutAsync(URL + "/ParamGlobal/update", content);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<ParamMotDePasseResource>();

                    return new JsonResult(new
                    {
                        title = "Paramétrage des mots de passe",
                        typeMessage = TypeMessage.Success.GetString(),
                        message = "Opération effectuée avec succès",
                        description = string.Empty,
                        timeOut = 8000,
                        strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<ParamMotDePasseResource>(
                           await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                    }, ConfigConstant.JsonSettings())
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };

                }
                var jQueryViewModel = await AppConstant.GetResponseMessage(result);
                return new JsonResult(new
                {
                    title = "Paramétrage des mots de passe",
                    typeMessage = TypeMessage.Error.GetString(),
                    message = "Erreur",
                    description = "Une erreur s'est produite",
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
        [Route("ParamMail")]
        public async Task<IActionResult> ParamMail(ParamMotDePasseViewModel model)
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
                var resource = new ParamMotDePasseResource()
                {
                    SmtpEmail = model.Resource.SmtpEmail,
                    SmtpName = model.Resource.SmtpName,
                    FromPassword = model.Resource.FromPassword,
                    SmtpClient = model.Resource.SmtpClient,
                    Port = model.Resource.Port,
                    Id = 1
                };
                var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
                var result = await httpClient.PutAsync(URL + "/ParamGlobal/smtp", content);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<ParamMotDePasseResource>();

                    return new JsonResult(new
                    {
                        title = "Paramétrage des mots de passe",
                        typeMessage = TypeMessage.Success.GetString(),
                        message = "Opération effectuée avec succès",
                        description = string.Empty,
                        timeOut = 8000,
                        strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<ParamMotDePasseResource>(
                           await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                    }, ConfigConstant.JsonSettings())
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };

                }
                var jQueryViewModel = await AppConstant.GetResponseMessage(result);
                return new JsonResult(new
                {
                    title = "Paramétrage des mots de passe",
                    typeMessage = TypeMessage.Error.GetString(),
                    message = "Erreur",
                    description = "Une erreur s'est produite",
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
        [Route("Client")]
        public async Task<IActionResult> Client(ClientViewModel model)
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
                var resource = new ClientResource()
                {
                    Nom = model.Client.Nom,
                    Prenoms = model.Client.Prenoms,
                    Indice = model.Client.Indice,
                    Email = model.Client.Email,
                    DateNaissance = model.Client.DateNaissance.ToString(),
                    Tel = model.Client.Tel,
                    Profession = model.Client.Profession,
                    Residence = model.Client.Residence,
                    Ville = model.Client.Ville,
                    Quartier = model.Client.Quartier,
                    LieuNaissance = model.Client.LieuNaissance,
                    AdressePostale = model.Client.AdressePostale
                };
                var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(URL1 + "/Client/add", content);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<ClientResource>();

                    return new JsonResult(new
                    {
                        title = "Ajout d'un client",
                        typeMessage = TypeMessage.Success.GetString(),
                        message = "Opération effectuée avec succès",
                        description = string.Empty,
                        timeOut = 8000,
                        strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<ClientResource>(
                           await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                    }, ConfigConstant.JsonSettings())
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };

                }
                var jQueryViewModel = await AppConstant.GetResponseMessage(result);
                return new JsonResult(new
                {
                    title = "Ajout d'un client",
                    typeMessage = TypeMessage.Error.GetString(),
                    message = "Erreur",
                    description = "Une erreur s'est produite",
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
        [Route("Compte")]
        public async Task<IActionResult> Compte(CompteViewModel model)
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
                var resource = new CompteResource()
                {
                    NumeroCompte = model.Compte.NumeroCompte,
                    ClientId = model.Compte.ClientId
                };
                var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(URL1 + "/Compte/add", content);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<CompteResource>();

                    return new JsonResult(new
                    {
                        title = "Ajout d'un compte",
                        typeMessage = TypeMessage.Success.GetString(),
                        message = "Opération effectuée avec succès",
                        description = string.Empty,
                        timeOut = 8000,
                        strJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<CompteResource>(
                           await result.Content.ReadAsStringAsync(), ConfigConstant.SetDateTimeConverter()))
                    }, ConfigConstant.JsonSettings())
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };

                }
                var jQueryViewModel = await AppConstant.GetResponseMessage(result);
                return new JsonResult(new
                {
                    title = "Ajout d'un compte",
                    typeMessage = TypeMessage.Error.GetString(),
                    message = "Erreur",
                    description = "Une erreur s'est produite",
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


    }
}
