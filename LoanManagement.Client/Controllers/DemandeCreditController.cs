using LoanManagement.Client.Resources;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.Client.Controllers
{
    [Route("DemandeCredit")]
    public class DemandeCreditController : Controller
    {
        private readonly ILogger<DemandeCreditController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient Client = new HttpClient();
        public DemandeCreditController(ILogger<DemandeCreditController> logger, 
            IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        private string UrlBase => MyConstants.LoanManagementApiUrl;
        [HttpGet]
        public IActionResult DemandeCredit()
        {
            ConfigConstant.AddCurrentRouteToSession(HttpContext);
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> DemandeCreditAction()
        {
            try
            {
                int nbrCigarettes = 0;
                int categorieSport = 0;
                string dateSurvenance = "";
                string natureInfirmite = "";
                bool fumer = false;
                bool sportif = false;
                bool infirme = false;
                var taille = Double.Parse(Request.Form["taille"].ToString());
                var poids = Double.Parse(Request.Form["poids"].ToString());
                var tension = Double.Parse(Request.Form["tension"].ToString());
                switch (Request.Form["fumer"].ToString())
                {
                    case "1":
                        fumer = true;
                        break;
                    case "0":
                        fumer = false;
                        break;
                    default:
                        break;
                }
                if (fumer) nbrCigarettes = int.Parse(Request.Form["oui"].ToString());
                var buveur = int.Parse(Request.Form["buveur"].ToString());
                var distractions = Request.Form["distraction"].ToString();
                switch (Request.Form["sport"].ToString())
                {
                    case "1":
                        sportif = true;
                        break;
                    case "0":
                        sportif = false;
                        break;
                    default:
                        break;
                }
                if (sportif) categorieSport = int.Parse(Request.Form["sportOui"].ToString());
                switch (Request.Form["infirmite"].ToString())
                {
                    case "1":
                        infirme = true;
                        break;
                    case "0":
                        infirme = false;
                        break;
                    default:
                        break;
                }
                if (infirme == true)
                {
                    dateSurvenance = Request.Form["infirmiteOuiDate"].ToString();
                    natureInfirmite = Request.Form["infirmiteOui"].ToString();
                }
                var echeancePiece = Request.Form["echeance"].ToString();
                var attestation = Request.Form.Files["attestationTravail"];
                var contrat = Request.Form.Files["contratTravail"];
                var premierBulletinSalaire = Request.Form.Files["premierBulletinSalaire"];
                var deuxiemeBulletinSalaire = Request.Form.Files["deuxiemeBulletinSalaire"];
                var troisiemeBulletinSalaire = Request.Form.Files["troisiemeBulletinSalaire"];
                var piece = Request.Form.Files["piece"];
                var factureProForma = Request.Form.Files["facture"];
                var dossier = new DossierClientResource()
                {
                    Taille = taille,
                    Poids = poids,
                    TensionArterielle = tension,
                    Fumeur = fumer,
                    NbrCigarettes = nbrCigarettes,
                    Distractions = distractions,
                    EstSportif = sportif,
                    EstInfirme = infirme,
                    CategorieSport = categorieSport,
                    NatureInfirmite = natureInfirmite,
                    DateSurvenance = dateSurvenance,
                    AttestationTravail = attestation,
                    ContratTravail = contrat,
                    PremierBulletinSalaire = premierBulletinSalaire,
                    DeuxiemeBulletinSalaire = deuxiemeBulletinSalaire,
                    TroisiemeBulletinSalaire = troisiemeBulletinSalaire,
                    FactureProFormat = factureProForma,
                    EcheanceCarteIdentite = echeancePiece,
                    CarteIdentite = piece,
                    ClientId = int.Parse(HttpContext.Session.GetString("_SESSIONID"))
                };
                switch (buveur)
                {
                    case 1:
                        dossier.BuveurOccasionnel = false;
                        dossier.BuveurRegulier = false;
                        break;
                    case 2:
                        dossier.BuveurOccasionnel = true;
                        dossier.BuveurRegulier = false;
                        break;
                    case 3:
                        dossier.BuveurOccasionnel = false;
                        dossier.BuveurRegulier = true;
                        break;
                    default:
                        break;
                }
                var content = new StringContent(JsonConvert.SerializeObject(dossier), Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(UrlBase + "/DossierClient/Add", content);
                if (response.IsSuccessStatusCode) return Ok();
                return Ok();
            }
            catch (Exception)
            {

                throw new Exception("Quelque chose s'est mal passé");
            }
        }
    }
}
