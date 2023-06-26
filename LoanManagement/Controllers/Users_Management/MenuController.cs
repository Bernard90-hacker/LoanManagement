using Constants.Pagination;
using FluentAssertions.Equivalency;
using LoanManagement.core.Models.Users_Management;

namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class MenuController : Controller
	{
		private readonly IMenuService _menuService;
		private readonly IMapper _mapper;
		private readonly IHabilitationProfilService _habilitationProfilService;
		private readonly ILoggerManager _logger;
		private readonly IApplicationService _applicationService;
        public MenuController(IMapper mapper, IMenuService menuService, 
			ILoggerManager logger, IApplicationService applicationService, IHabilitationProfilService profil)
        {
			_mapper = mapper;
			_menuService = menuService;
			_logger = logger;
			_applicationService = applicationService;
			_habilitationProfilService = profil;
        }

		[HttpGet("all")]
		public async Task<ActionResult<PagedList<MenuRessource>>> GetAll([FromQuery] MenuParameters parameters)
		{
			try
			{
				var menus = await _menuService.GetAll(parameters);
				if(menus is null)
				{
					_logger.LogWarning("Détails d'un menu : Aucun menu trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun menu trouvé"));
				}
				var menuRessource = _mapper.Map<IEnumerable<MenuRessource>>(menus);
				var metadata = new
				{
					menus.PageSize,
					menus.CurrentPage,
					menus.TotalCount,
					menus.TotalPages,
					menus.HasNext,
					menus.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation("Liste des menus : Opération effectuée avec succès");
				return Ok(menuRessource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<PagedList<MenuRessource>>> GetMenuById(int id)
		{
			try
			{
				var menu = await _menuService.GetMenuById(id);
				if(menu is null)
				{
					_logger.LogWarning("'Détails d'un menu' : Menu inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Menu inexistant"));
				}
				var ressource = _mapper.Map<MenuRessource>(menu);
				_logger.LogInformation("Détails d'un menu : Opération effectuée avec succès");
				return Ok(ressource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{code}")]
		public async Task<ActionResult<PagedList<MenuRessource>>> GetMenuByCode(string code)
		{
			try
			{
				var menu = await _menuService.GetMenuByCode(code);
				if (menu is null)
				{
					_logger.LogWarning("'Détails d'un menu' : Menu inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Menu inexistant"));
				}
				var ressource = _mapper.Map<MenuRessource>(menu);
				_logger.LogInformation("Détails d'un menu : Opération effectuée avec succès");
				return Ok(ressource);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult<MenuRessource>> Add(MenuRessource ressource)
		{
			try
			{
				var validator = new MenuRessourceValidator();
				var menuDb = _mapper.Map<Menu>(ressource);
				var menuPosition = await _menuService.GetMenuByPosition(ressource.Position);
				var validationResult = await validator.ValidateAsync(ressource);
				var droit = await _habilitationProfilService.GetHabilitationProfilById((int)ressource.HabilitationProfilId);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Enregistrement d'un sous-menu/menu : Champs obligatoires");
					return BadRequest();
				}
				if(ressource.MenuId != 0)
				{
					var menu = await _menuService.GetMenuById((int)ressource.MenuId);
					if(menu is null)
					{
						_logger.LogWarning("Enregistrement d'un sous-menu : Le menu principal indiqué est introuvable");
						return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Menu principal indiqué introuvable"));
					}
				}
				else menuDb.MenuId = null;

				if(menuPosition is not null) //Le menu ne doit pas occuper une position déjà prise.
				{
					_logger.LogWarning("Enregistrement d'un sous-menu : Un autre menu occupe la position indiquée");
					return BadRequest(new ApiResponse((int)CustomHttpCode.WARNING, description: "Un autre menu occupe la position indiquée"));
				}
				if(droit is null)
				{
					_logger.LogWarning("Enregistrement d'un sous-menu/menu : Le droit spécifié est introuvable");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Le droit spécifié est introuvable"));
				}

				var application = await _applicationService.GetApplicationById(ressource.ApplicationId);
				if(application is null)
				{
					_logger.LogWarning("Enregistrement d'un menu ou sous-menu: Le module spécifié est introuvable");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Module indiqué introuvable"));
				}
				var menus = (await _menuService.GetAll());
				string reference = string.Empty;
				if (menus.Count() == 0)
					reference = "000";
				else
					reference = menus.LastOrDefault().Code.Substring(7);
				menuDb.Code = "MENU - " + Constants.Utils.UtilsConstant.IncrementStringWithNumbers(reference);
				menuDb.DateAjout = DateTime.Now.ToString("dd/MM/yyyy");
				var menuCreated = await _menuService.Create(menuDb);
				var result = _mapper.Map<MenuRessource>(menuCreated);
				_logger.LogInformation("Enregistrement d'un menu : Opération effectuée avec succès");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("updateMenuStatut")]
		public async Task<ActionResult<MenuRessource>> UpdateMenuStatut(UpdateMenuStatutRessource ressource)
		{
			try
			{
				var menu = await _menuService.GetMenuById(ressource.Id);
				if(menu is null)
				{
					_logger.LogWarning("Modification d'un sous-menu/menu : Le menu indiqué est introuvable");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Menu indiqué introuvable"));
				}
				var menuUpdated = await _menuService.UpdateMenuStatut(menu, ressource.statut);
				var menuResult = _mapper.Map<MenuRessource>(menuUpdated);
				_logger.LogInformation("Modification d'un menu/sous-menu : Opération effectuée avec succès");

				return Ok(menuResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				var menu = await _menuService.GetMenuById(id);
				if (menu is null)
				{
					_logger.LogWarning("Suppression d'un sous-menu/menu : Le menu indiqué est introuvable");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Menu indiqué introuvable"));
				}
				var sousmenus = await _menuService.GetSousMenus(id);
				if(sousmenus.Count() > 1) 
				{
					_logger.LogWarning("Suppression d'un menu : Impossible d'effectuer cette action, le menu a des sous-menus");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Impossible d'effectuer cette action, le menu a des sous-menus"));
				}
				await _menuService.Delete(menu);
				_logger.LogInformation("Suppression d'un menu/sous-menu : Opération effectuée avec succès");
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}
	}
}
