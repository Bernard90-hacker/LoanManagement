using Constants.Pagination;

namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class ProfilController : Controller
	{
		private readonly IProfilService _profilService;
		private readonly IUtilisateurService _utilisateurService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;

		public ProfilController(IProfilService profilService, ILoggerManager logger,
			IMapper mapper, IUtilisateurService utilisateurService)
		{
			_profilService = profilService;
			_logger = logger;
			_mapper = mapper;
			_utilisateurService = utilisateurService;
		}

		[HttpGet("all")]
		public async Task<ActionResult<PagedList<ProfilRessource>>> GetAll([FromQuery] ProfilParameters parameters)
		{
			try
			{
				var profils = await _profilService.GetAll(parameters);
				if (profils is null)
				{
					_logger.LogWarning("'Détails des profils' : Aucun profil trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}
				var result = _mapper.Map<IEnumerable<ProfilRessource>>(profils);
				var metadata = new
				{
					profils.PageSize,
					profils.CurrentPage,
					profils.TotalCount,
					profils.TotalPages,
					profils.HasNext,
					profils.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation($"'Liste des utilisateurs ': Opération effectuée avec succès, {profils.Count} utilisateurs retournés");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}

		}

		[HttpGet("disabled")]
		public async Task<ActionResult<PagedList<ProfilRessource>>> GetDisabledProfiles()
		{
			try
			{
				var profils = await _profilService.GetDisabledProfiles();
				if (profils is null)
				{
					_logger.LogWarning("'Liste des profils désactivés' : Aucun profil trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}
				var result = _mapper.Map<IEnumerable<ProfilRessource>>(profils);
				var metadata = new
				{
					profils.PageSize,
					profils.CurrentPage,
					profils.TotalCount,
					profils.TotalPages,
					profils.HasNext,
					profils.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation($"'Liste des profils désactivés ': Opération effectuée avec succès, {profils.Count} utilisateurs retournés");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}

		}

		[HttpGet("activated")]
		public async Task<ActionResult<PagedList<ProfilRessource>>> GetActivatedProfiles()
		{
			try
			{
				var profils = await _profilService.GetActivatedProfiles();
				if (profils is null)
				{
					_logger.LogWarning("'Liste des profils actifs' : Aucun profil trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Utilisateur(s) non trouvé(s)"));
				}
				var result = _mapper.Map<IEnumerable<ProfilRessource>>(profils);
				var metadata = new
				{
					profils.PageSize,
					profils.CurrentPage,
					profils.TotalCount,
					profils.TotalPages,
					profils.HasNext,
					profils.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation($"'Liste des profils actifs ': Opération effectuée avec succès, {profils.Count} utilisateurs retournés");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}

		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<ProfilRessource>> GetProfilById(int id)
		{
			try
			{
				var profil = await _profilService.GetProfilById(id);
				if (profil is null)
				{
					_logger.LogWarning("'Détails d'un profil' : Aucun profil trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Profil(s) non trouvé(s)"));
				}
				var result = _mapper.Map<ProfilRessource>(profil);
				_logger.LogInformation($"'Détails d'un profil ': Opération effectuée avec succès");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("code")]
		public async Task<ActionResult<ProfilRessource>> GetProfilByCode(string code)
		{
			try
			{
				var profil = await _profilService.GetProfilByCode(code);
				if (profil is null)
				{
					_logger.LogWarning("'Détails d'un profil' : Aucun profil trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Profil(s) non trouvé(s)"));
				}
				var result = _mapper.Map<ProfilRessource>(profil);
				_logger.LogInformation($"'Détails d'un profil ': Opération effectuée avec succès");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult<ProfilRessource>> Add(ProfilRessource ressource)
		{
			try
			{
				//Validation
				var validation = new ProfilValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				var user = await _utilisateurService.GetUserById(ressource.UtilisateurId);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Enregistrement d'un profil : champs obligatoires");
					return BadRequest();
				}
				if (user is null)
				{
					_logger.LogWarning("Enregistrement d'un profil : Utilisateur non trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun utilisateur trouvé"));
				}
				var profil = _mapper.Map<Profil>(ressource);
				profil.UtilisateurId = user.Id;
				//Enregistrement
				var profilCreated = await _profilService.Create(profil);

				//Mappage en vue de retourner la ressource à l'utilisateur
				var profilResult = _mapper.Map<ProfilRessource>(profilCreated);

				_logger.LogInformation("Enregistrement d'un profil : Opération effectuée avec succès");
				return Ok(profilResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPatch("updateExpiryDate")]
		public async Task<ActionResult<ProfilRessource>> UpdateExpiryDate(UpdateProfilExpiryDateRessource ressource)
		{
			try
			{

				var profil = await _profilService.GetProfilById(ressource.ProfilId);
				if (profil is null)
				{
					_logger.LogWarning("Modification d'un profil : Profil inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun profil trouvé"));
				}
				//Mise à jour
				var profilUpdated = await _profilService.UpdateProfilExpiryDate(profil, ressource.DateExpiration);

				//Mappage en vue de retourner la ressource à l'utilisateur
				var profilResult = _mapper.Map<ProfilRessource>(profilUpdated);

				_logger.LogInformation("Mise à jour de la date d'expiration d'un profil : Opération effectuée avec succès");
				return Ok(profilResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPatch("updateStatus")]
		public async Task<ActionResult<ProfilRessource>> UpdateStatus(UpdateProfilStatusRessource ressource)
		{
			try
			{

				var profil = await _profilService.GetProfilById(ressource.ProfilId);
				if (profil is null)
				{
					_logger.LogWarning("Modification d'un profil : Profil inexistant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun profil trouvé"));
				}
				var profilUpdated = await _profilService.UpdateProfilStatus(profil, ressource.Statut);

				//Mappage en vue de retourner la ressource à l'utilisateur
				var profilResult = _mapper.Map<ProfilRessource>(profilUpdated);

				_logger.LogInformation("Mise à jour du statut d'un profil : Opération effectuée avec succès");
				return Ok(profilResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<IEnumerable<ProfilRessource>>> Delete(int id)
		{
			try
			{
				var profil = await _profilService.GetProfilById(id);
				if (profil is null)
				{
					_logger.LogWarning("'Détails d'un profil' : Aucun profil trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Profil(s) non trouvé(s)"));
				}
				await _profilService.Delete(profil);

				_logger.LogInformation($"'Suppression d'un profil ': Opération effectuée avec succès");
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{id}/habilitation")]
		public async Task<ActionResult<HabilitationProfilRessource>> GetProfilHabilitation(int id)
		{
			try
			{
				var profil = await _profilService.GetProfilById(id);
				if(profil is null)
				{
					_logger.LogWarning("'Détails d'un profil' : Aucun profil trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun profil trouvé"));
				}

				var habilitationProfil = await _profilService.GetProfilHabilitation(profil.Id);
				if(habilitationProfil is null)
				{
					_logger.LogWarning($"'Habilitation du profil {profil.Code}' : Aucune habilitation affectée");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucune habilitation affectée"));
				}
				var result = _mapper.Map<HabilitationProfilRessource>(habilitationProfil);

				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}
	}

}
