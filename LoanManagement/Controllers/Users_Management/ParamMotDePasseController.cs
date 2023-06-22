namespace LoanManagement.API.Controllers.Users_Management
{
    [ApiController]
	[Route("api/users/[controller]")]
	public class ParamMotDePasseController : Controller
	{
		private readonly IParamMotDePasseService _paramMotDePasseService;
		private readonly IMotDePasseService _motDePasseService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;

        public ParamMotDePasseController(IParamMotDePasseService param, ILoggerManager logger, 
			IMapper mapper, IMotDePasseService motDePasseService)
        {
			_paramMotDePasseService = param;
			_logger = logger;
			_mapper = mapper;
			_motDePasseService = motDePasseService;
        }

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<ParamMotDePasseRessource>>> GetAll()
		{
			try
			{
				var param = await _paramMotDePasseService.GetAll();
				if(param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var result = _mapper.Map<IEnumerable<ParamMotDePasseRessource>>(param);
				_logger.LogInformation("Récupération des configurations des mots de passe : Opération effectuée avec succès");

				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult<ParamMotDePasseRessource>> Add(ParamMotDePasseRessource ressource)
		{
			try
			{
				var validation = new ParamMotDePasseValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Configuration des paramètres des mots de passe : Champs Obligatoires");
					return BadRequest();
				}
				var param = await _paramMotDePasseService.GetAll();
				if(param.Count() == 1)
				{
					_logger.LogWarning("Configuration des paramètres des mots de passe : Impossible d'ajouter plus d'une configuration");
					return BadRequest();
				}

				var paramDb = _mapper.Map<ParamMotDePasse>(ressource);
				var paramAdded = await _paramMotDePasseService.Create(paramDb);
				var result = _mapper.Map<ParamMotDePasseRessource>(paramAdded);
				_logger.LogInformation("Configuration des paramètres des mots de passe : Opération effectuée avec succès");

				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPatch("update")]
		public async Task<ActionResult<ParamMotDePasseRessource>> UpdatePasswordsExpiryFrequency(ParamMotDePasseUpdateExpiryDateRessource ressource)
		{
			try
			{
				var param = await _paramMotDePasseService.GetById(ressource.Id);
				if(param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var paramUpdated = await _paramMotDePasseService.UpdatePasswordsExpiryFrequency(param, ressource.ExpiryFrequency);
				var paramResult = _mapper.Map<ParamMotDePasseRessource>(paramUpdated);

				_logger.LogInformation("Modification du délai d'expiration des mots de passe : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPatch("IncludeDigits")]
		public async Task<ActionResult<ParamMotDePasseRessource>> PasswordsMustIncludeDigits(PasswordConfigurationRessource ressource)
		{
			try
			{
				var param = await _paramMotDePasseService.GetById(ressource.Id);
				if (param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var paramUpdated = await _paramMotDePasseService.PasswordMustIncludeDigits(param, ressource.Response);
				var paramResult = _mapper.Map<ParamMotDePasseRessource>(paramUpdated);

				_logger.LogInformation("Modification du délai d'expiration des mots de passe : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPatch("IncludeLowerCase")]
		public async Task<ActionResult<ParamMotDePasseRessource>> PasswordsMustIncludeUpperCase(PasswordConfigurationRessource ressource)
		{
			try
			{
				var param = await _paramMotDePasseService.GetById(ressource.Id);
				if (param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var paramUpdated = await _paramMotDePasseService.PasswordMustIncludeUpperCase(param, ressource.Response);
				var paramResult = _mapper.Map<ParamMotDePasseRessource>(paramUpdated);

				_logger.LogInformation("Modification du délai d'expiration des mots de passe : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPatch("ExcludeUsername")]
		public async Task<ActionResult<ParamMotDePasseRessource>> PasswordsMustExcludeUsername(PasswordConfigurationRessource ressource)
		{
			try
			{
				var param = await _paramMotDePasseService.GetById(ressource.Id);
				if (param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var paramUpdated = await _paramMotDePasseService.PasswordMustExcludeUsername(param, ressource.Response);
				var paramResult = _mapper.Map<ParamMotDePasseRessource>(paramUpdated);

				_logger.LogInformation("Modification du délai d'expiration des mots de passe : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPatch("PasswordLength")]
		public async Task<ActionResult<ParamMotDePasseRessource>> PasswordsLength(PasswordLengthConfigurationRessource ressource)
		{
			try
			{
				var param = await _paramMotDePasseService.GetById(ressource.Id);
				if (param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var paramUpdated = await _paramMotDePasseService.PasswordLength(param, ressource.Taille);
				var paramResult = _mapper.Map<ParamMotDePasseRessource>(paramUpdated);

				_logger.LogInformation("Modification du délai d'expiration des mots de passe : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<ParamMotDePasseRessource>> Delete(int id)
		{
			try
			{
				var passwords = await _paramMotDePasseService.GetAll();
				if (passwords is not null)
				{
					_logger.LogWarning("Suppression des paramètres mot de passe : Impossible d'exécuter cette requête");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Impossible d'exécuter cette requête, tous les mots de passe existants dépendent de cette configuration, veuillez procéder à une modification"));
				}
				var param = await _paramMotDePasseService.GetById(id);
				if(param is null)
				{
					_logger.LogWarning("Suppression des paramètres mot de passe :Aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				await _paramMotDePasseService.Delete(param);

				_logger.LogInformation("Suppression des paramètres mot de passe: Opération effectuée avec succès");
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
