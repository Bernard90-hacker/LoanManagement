namespace LoanManagement.API.Controllers.Users_Management
{
    [ApiController]
	[Route("api/users/[controller]")]
	public class ParamGlobalController : Controller
	{
		private readonly IParamGlobalService _paramGlobalService;
		private readonly IMotDePasseService _motDePasseService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly JournalisationService _journalisationService;

        public ParamGlobalController(IParamGlobalService param, ILoggerManager logger, 
			IMapper mapper, IMotDePasseService motDePasseService, JournalisationService service)
        {
			_paramGlobalService = param;
			_logger = logger;
			_mapper = mapper;
			_motDePasseService = motDePasseService;
			_journalisationService = service;
        }

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<ParamGlobalRessource>>> GetAll()
		{
			try
			{
				var param = await _paramGlobalService.GetAll();
				if(param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var result = _mapper.Map<IEnumerable<ParamGlobalRessource>>(param);
				_logger.LogInformation("Récupération des configurations des mots de passe : Opération effectuée avec succès");
				//var journal = new Journal() { Libelle = "Liste des paramètres de configuration des mots de passe" };
				//await _journalisationService.Journalize(journal);
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult<ParamGlobalRessource>> Add(ParamGlobalRessource ressource)
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
				var param = await _paramGlobalService.GetAll();
				if(param.Count() == 1)
				{
					_logger.LogWarning("Configuration des paramètres des mots de passe : Impossible d'ajouter plus d'une configuration");
					return BadRequest();
				}

				var paramDb = _mapper.Map<ParamGlobal>(ressource);
				var paramAdded = await _paramGlobalService.Create(paramDb);
				
				var result = _mapper.Map<ParamGlobalRessource>(paramAdded);
				_logger.LogInformation("Configuration des paramètres des mots de passe : Opération effectuée avec succès");

				return Ok(result);
			}
			catch (Exception ex)
			{
				
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}
        [HttpPut("update")]
        public async Task<ActionResult<ParamGlobalRessource>> Update(UpdateParamMotDePasseRessource ressource)
        {
            try
            {
				var validation = new UpdateParamMotDePasseValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Mise à des paramètres des mots de passe : Champs Obligatoires");
					return BadRequest();
				}
				var paramToBeUpdated = await _paramGlobalService.GetById(ressource.Id);
                if (paramToBeUpdated is null)
                {
                    _logger.LogWarning("Paramètre global : aucun paramètre existant");
                    return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
                }

                var param = _mapper.Map<ParamGlobal>(ressource);
                var paramUpdated = await _paramGlobalService.UpdatePwdParam(param, paramToBeUpdated);
                _logger.LogInformation("Mise à jour des paramètres : Opération effectuée avec succès");

                return Ok(paramUpdated);
            }
            catch (Exception ex)
            {

                _logger.LogError("Une erreur est survenue pendant le traitement de la requête");
                return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
            }
        }

        [HttpPut("smtp")]
		public async Task<ActionResult> Update(SmtpRessource ressource)
		{
			try
			{
				var paramToBeUpdated = await _paramGlobalService.GetById(ressource.Id);
				if(paramToBeUpdated is null)
				{
                    _logger.LogWarning("Paramètre global : aucun paramètre existant");
                    return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
                }
				var param = _mapper.Map<ParamGlobal>(ressource);
				var paramUpdated = await _paramGlobalService.Update(param, paramToBeUpdated);
				_logger.LogInformation("Mise à jour des paramètres : Opération effectuée avec succès");

				return Ok(paramUpdated);
			}
			catch (Exception ex)
			{
                _logger.LogError("Une erreur est survenue pendant le traitement de la requête");
                return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
            }
		}

		[HttpPut("updateFrequency")]
		public async Task<ActionResult<ParamGlobalRessource>> UpdatePasswordsExpiryFrequency(ParamMotDePasseUpdateExpiryDateRessource ressource)
		{
			try
			{
				var param = await _paramGlobalService.GetById(ressource.Id);
				if(param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var paramUpdated = await _paramGlobalService.UpdatePasswordsExpiryFrequency(param, ressource.ExpiryFrequency);
				var journal = new Journal() { Libelle = "Mise à jour de la fréquence d'expiration des mots de passe" };
				await _journalisationService.Journalize(journal);
				var paramResult = _mapper.Map<ParamGlobalRessource>(paramUpdated);

				_logger.LogInformation("Modification du délai d'expiration des mots de passe : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("IncludeDigits")]
		public async Task<ActionResult<ParamGlobalRessource>> PasswordsMustIncludeDigits(PasswordConfigurationRessource ressource)
		{
			try
			{
				var param = await _paramGlobalService.GetById(ressource.Id);
				if (param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var paramUpdated = await _paramGlobalService.PasswordMustIncludeDigits(param, ressource.Response);
				
				var paramResult = _mapper.Map<ParamGlobalRessource>(paramUpdated);
				//var journal = new Journal() { Libelle = "Modification des paramètres des mots de passe : Doit-il contenir des chiffres" };
				//await _journalisationService.Journalize(journal);
				_logger.LogInformation("Modification du délai d'expiration des mots de passe : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("IncludeLowerCase")]
		public async Task<ActionResult<ParamGlobalRessource>> PasswordsMustIncludeUpperCase(PasswordConfigurationRessource ressource)
		{
			try
			{
				var param = await _paramGlobalService.GetById(ressource.Id);
				if (param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var paramUpdated = await _paramGlobalService.PasswordMustIncludeUpperCase(param, ressource.Response);
				
				var paramResult = _mapper.Map<ParamGlobalRessource>(paramUpdated);
				var journal = new Journal() { Libelle = "Modification des paramètres des mots de passe : Doit-il contenir des caractères majuscules" };
				_logger.LogInformation("Modification du délai d'expiration des mots de passe : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("ExcludeUsername")]
		public async Task<ActionResult<ParamGlobalRessource>> PasswordsMustExcludeUsername(PasswordConfigurationRessource ressource)
		{
			try
			{
				var param = await _paramGlobalService.GetById(ressource.Id);
				if (param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var paramUpdated = await _paramGlobalService.PasswordMustExcludeUsername(param, ressource.Response);
				
				var paramResult = _mapper.Map<ParamGlobalRessource>(paramUpdated);
				var journal = new Journal() { Libelle = "Modification des paramètres des mots de passe :Exclusion du username" };
				_logger.LogInformation("Modification du délai d'expiration des mots de passe : Opération effectuée avec succès");
				return Ok(paramResult);
			}
			catch (Exception ex)
			{
				
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("PasswordLength")]
		public async Task<ActionResult<ParamGlobalRessource>> PasswordsLength(PasswordLengthConfigurationRessource ressource)
		{
			try
			{
				var param = await _paramGlobalService.GetById(ressource.Id);
				if (param is null)
				{
					_logger.LogWarning("Paramètres mot de passe : aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				var paramUpdated = await _paramGlobalService.PasswordLength(param, ressource.Taille);
				
				var paramResult = _mapper.Map<ParamGlobalRessource>(paramUpdated);
				//var journal = new Journal() { Libelle = "Modification de la taille des mots de passe" };
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
		public async Task<ActionResult<ParamGlobalRessource>> Delete(int id)
		{
			try
			{
				var passwords = await _paramGlobalService.GetAll();
				if (passwords is not null)
				{
					_logger.LogWarning("Suppression des paramètres mot de passe : Impossible d'exécuter cette requête");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Impossible d'exécuter cette requête, tous les mots de passe existants dépendent de cette configuration, veuillez procéder à une modification"));
				}
				var param = await _paramGlobalService.GetById(id);
				if(param is null)
				{
					_logger.LogWarning("Suppression des paramètres mot de passe :Aucun paramètre existant");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun paramètre existant"));
				}
				await _paramGlobalService.Delete(param);
				//var journal = new Journal() { Libelle = "Suppression des paramètres des mots de passe" };

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
