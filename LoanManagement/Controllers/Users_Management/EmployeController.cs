using Constants.Config;

namespace LoanManagement.API.Controllers.Users_Management
{
	[ApiController]
	[Route("api/users/[controller]")]
	public class EmployeController : Controller
	{
		private readonly IUtilisateurService _utilisateurService;
		private readonly IEmployeService _employeService;
		private readonly IDepartmentService _departementService;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly JournalisationService _journalisationService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeController(IUtilisateurService utilisateurService, IEmployeService employeService,
			ILoggerManager logger, IMapper mapper, IDepartmentService departementService,
            JournalisationService service, IWebHostEnvironment webHostEnvironment)
        {
            _utilisateurService = utilisateurService;
            _employeService = employeService;
            _logger = logger;
            _mapper = mapper;
            _departementService = departementService;
            _journalisationService = service;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("all")]
		public async Task<ActionResult<EmployeRessource>> GetAll([FromQuery] EmployeParameters parameters)
		{
			try
			{
				var employes = await _employeService.GetAll(parameters);
				if(employes is null)
				{
					_logger.LogWarning("'Liste des employés' : Aucun employé trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun employé trouvé qui corresponde au(x) critère(s) spécifié(s)"));
				}
				var result = _mapper.Map<IEnumerable<GetEmployeResource>>(employes);
				var metadata = new
				{
					employes.PageSize,
					employes.CurrentPage,
					employes.TotalCount,
					employes.TotalPages,
					employes.HasNext,
					employes.HasPrevious
				};
				Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
				_logger.LogInformation("'Liste des employés' : Opération effectuée avec succès");
				var journal = new Journal() { Libelle = "Liste des employés", TypeJournalId = 8, Entite="User" };
				await _journalisationService.Journalize(journal);
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<EmployeRessource>> GetEmployeById(int id)
		{
			try
			{
				var employe = await _employeService.GetEmployeById(id);
				if(employe is null)
				{
					_logger.LogWarning("Détails d'un employé : aucun employé trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun employé trouvé qui correspond au critère spécifié"));
				}

				var result = _mapper.Map<GetEmployeResource>(employe);
				_logger.LogInformation("'Détails d'un employé' : Opération effectuée avec succès");
				var journal = new Journal() { Libelle = "Identification d'un employé par son id" };
				await _journalisationService.Journalize(journal);
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{matricule:int}")]
		public async Task<ActionResult<EmployeRessource>> GetEmployeByMatricule(int matricule)
		{
			try
			{
				var employe = await _employeService.GetEmployeByMatricule(matricule);
				if (employe is null)
				{
					_logger.LogWarning("Détails d'un employé : aucun employé trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun employé trouvé qui correspond au critère spécifié"));
				}

				var result = _mapper.Map<GetEmployeResource>(employe);
				_logger.LogInformation("'Détails d'un employé' : Opération effectuée avec succès");
				var journal = new Journal() { Libelle = "Identification d'un employé par son matricule" };
				await _journalisationService.Journalize(journal);
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{id:int}/Utilisateur")]
		public async Task<ActionResult<UtilisateurRessource>> GetEmployeUserAccount(int id)
		{
			try
			{
				var employe = await _employeService.GetEmployeById(id);
				if (employe is null)
				{
					_logger.LogWarning("Détails d'un employé : aucun employé trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun employé trouvé qui correspond au critère spécifié"));
				}
				var user = await _employeService.GetEmployeUserAccount(employe.UserId);
				var journal = new Journal() { Libelle = "Identification du compte utilisateur d'un employé par son id" };
				await _journalisationService.Journalize(journal);
				var result = _mapper.Map<UtilisateurRessource>(user);
				_logger.LogInformation("'Détails du compte utilisateur d'un employé' : Opération effectuée avec succès");

				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpGet("{email}")]
		public async Task<ActionResult<EmployeRessource>> GetEmployeByEmail(string email)
		{
			try
			{
				var employe = await _employeService.GetEmployeByEmail(email);
				if (employe is null)
				{
					_logger.LogWarning("Détails d'un employé : aucun employé trouvé");
					return NotFound(new ApiResponse((int)CustomHttpCode.OBJECT_NOT_FOUND, description: "Aucun employé trouvé qui corresponde au critère spécifié"));
				}

				var result = _mapper.Map<GetEmployeResource>(employe);
				_logger.LogInformation("'Détails d'un employé' : Opération effectuée avec succès");
				
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPost("add")]
		public async Task<ActionResult<EmployeRessource>> Add([FromForm] EmployeRessource ressource)
		{
			try
			{
				var validation = new EmployeValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogWarning("Enregistrement d'un employé : Champs obligatoires");
					return BadRequest();
				}
				var user = await _utilisateurService.GetUserByUsername(ressource.Username);
				var departement = await _departementService.GetDepartmentById(ressource.DepartementId);
				var nomComplet = $"{ressource.Nom} {ressource.Prenoms}";
				var employeFullName = await _employeService.GetEmployeeByFullName(nomComplet);
				if(employeFullName is not null)
				{
					_logger.LogWarning("Enregistrement d'un employé : Nom et prénoms de l'employé existe déjà");
					return BadRequest("Nom et prénoms de l'employé existe déjà");
				}
				if(user is null || departement is null)
				{
					_logger.LogWarning("Enregistrement d'un employé : L'utilisateur ou le département renseigné n'existe pas");
					return BadRequest("L'utilisateur ou le département renseigné n'existe pas");
				}
				var employe = await _employeService.GetEmployeByEmail(ressource.Email);
				if(employe is not null)
				{
					_logger.LogWarning("Enregistrement d'un employé : Un employé avec le même email existe, veuillez utiliser un autre");
					return BadRequest("Un employé avec le même email existe, veuillez utiliser un autre");
				}
				var didUserAlreadyAfectedToEmploye = await _employeService.GetEmployeUserAccount(user.Id);
				if (didUserAlreadyAfectedToEmploye is not null)
				{
					_logger.LogWarning("Enregistrement d'un employé : Un employé avec le même compte existe, veuillez utiliser un autre");
					return BadRequest("Un employé avec le même compte existe, veuillez utiliser un autre");
				}
				var employeDb = _mapper.Map<Employe>(ressource);
				employeDb.UserId = user.Id;
				employeDb.DateAjout = DateTime.Now.ToString("dd/MM/yyyy");
				employeDb.DateModification = DateTime.Now.ToString("dd/MM/yyyy");
                if (!ressource.Photo.IsNull())
                {
                    employeDb.Photo = await ConfigConstants.UploadApiFile(_webHostEnvironment, ressource.Photo, GlobalConstants.PhotoUtilisateur);
                    if (employeDb.Photo.Equals("NotAccepted"))
                    {
                        _logger.LogWarning("'Ajout d'un agent' : format photo non valide.");
                        return NotFound(new ApiResponse((int)CustomHttpCode.ERROR, description: "Format de la photo non valide !! Seuls sont autorisés les formats png, jpeg ou jpg."));
                    }
                }

                var employeCreated = await _employeService.Create(employeDb);
				
				var result = _mapper.Map<GetEmployeResource>(employeCreated);
				var journal = new Journal() { Libelle = "Enregistrement d'un employé", TypeJournalId = 5, Entite="User" };
				await _journalisationService.Journalize(journal);
				_logger.LogInformation("'Enregistrement d'un employé' : Opération effectuée avec succès");

				return Ok(result);
			}
			catch (Exception ex)
			{
				
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<EmployeRessource>> Delete(int id)
		{
			try
			{
				var employe = await _employeService.GetEmployeById(id);
				if (employe is null)
				{
					_logger.LogWarning("Suppression d'un employé : Aucun employé trouvé");
					return BadRequest();
				}
				await _employeService.Delete(employe);
				var journal = new Journal() { Libelle = "Suppression d'un employé" };
				await _journalisationService.Journalize(journal);
				_logger.LogInformation("'Suppression d'un employé' : Opération effectuée avec succès");

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("photo")]
		public async Task<ActionResult> UpdatePhoto([FromForm] UpdateEmployePhotoRessource ressource)
		{
			try
			{
				var employe = await _employeService.GetEmployeById(ressource.Id);
				string photo = "";
				if (employe is null)
				{
					_logger.LogWarning("Suppression d'un employé : Aucun employé trouvé");
					return BadRequest();
				}
                if (!ressource.Photo.IsNull())
                {
                    photo = await ConfigConstants.UploadApiFile(_webHostEnvironment, ressource.Photo, GlobalConstants.PhotoUtilisateur);
                    if (photo.Equals("NotAccepted"))
                    {
                        _logger.LogWarning("'Ajout d'un agent' : format photo non valide.");
                        return NotFound(new ApiResponse((int)CustomHttpCode.ERROR, description: "Format de la photo non valide !! Seuls sont autorisés les formats png, jpeg ou jpg."));
                    }
                }
                var employeUpdated = await _employeService.UpdateEmployePhoto(employe, photo);
                var result = _mapper.Map<GetEmployeResource>(employeUpdated);
				//var journal = new Journal() { Libelle = "Mise à jour de la photo d'un employé" };
				//await _journalisationService.Journalize(journal);

				_logger.LogInformation("'Mise à jour de la photo d'un employé' : Opération effectuée avec succès");

				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur interne du serveur", detail: ex.Message);
			}
		}

		[HttpPut("departement")]
		public async Task<ActionResult<EmployeRessource>> UpdateEmployeDepartement(UpdateEmployeDepartementRessource ressource)
		{
			try
			{
				var employe = await _employeService.GetEmployeById(ressource.Id);
				var departement = await _departementService.GetDepartmentById(ressource.DepartementId);
				if (employe is null || departement is null)
				{
					_logger.LogWarning("Suppression d'un employé : Aucun employé ou département trouvé");
					return BadRequest();
				}
				var employeUpdated = await _employeService.UpdateEmployeDepartment(employe, ressource.DepartementId);
				var result = _mapper.Map<GetEmployeResource>(employeUpdated);
				var journal = new Journal() { Libelle = "Modification du département d'un employé" };
				await _journalisationService.Journalize(journal);
				_logger.LogInformation("'Mise à jour de la modification de la photo d'un employé' : Opération effectuée avec succès");

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
