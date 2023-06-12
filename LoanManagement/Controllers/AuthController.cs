namespace LoanManagement.API.Controllers
{

	[ApiController]
	[Route("api/LoanManagement/")]
	public class AuthController : Controller
	{
		private readonly IUserService _userService;
		private readonly UserService _service;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly MailService _mailService;
		private readonly TokenService _tokenService;
		private readonly IConfiguration _config;

		public AuthController(IMapper mapper, IUserService userService,
			UserService service, ILoggerManager logger, MailService mailService,
			TokenService tokenService, IConfiguration config)
		{
			_userService = userService;
			_service = service;
			_logger = logger;
			_mapper = mapper;
			_tokenService = tokenService;
			_mailService = mailService;
			_config = config;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginRessource ressource)
		{
			try
			{
				var validator = new LoginValidator();
				var validatorResult = await validator.ValidateAsync(ressource);
				if (!validatorResult.IsValid)
				{
					_logger.LogError("Connexion d'un utilisateur : Champs obligatoires");
					return BadRequest();
				}

				var dto = _mapper.Map<LoginDto>(ressource);
				User? user = await _service.Login(dto);
				if (user is null)
					return Unauthorized("Invalid credentials");

				string accessToken = _tokenService.CreateAccessToken(user.Id,
					_config.GetSection("JWT:AccessToken").Value);

				string refreshToken = _tokenService.CreateRefreshToken(user.Id,
					_config.GetSection("JWT:RefreshToken").Value);

				CookieOptions cookieOptions = new();
				cookieOptions.HttpOnly = true; //This will make the cookies only accessible by the backend.
				Response.Cookies.Append("refresh_token", refreshToken, cookieOptions);

				UserToken userToken = new UserToken()
				{
					UserId = user.Id,
					Token = refreshToken,
					CreatedAt = DateTime.Now,
					ExpiredAt = DateTime.Now.AddDays(7)
				};

				_logger.LogInformation("Connexion d'un utilisateur : Opération effectuée avec succès");
				await _tokenService.Add(userToken);

				return Ok(new
				{
					token = accessToken
				});

			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête, 'Erreur Interne du serveur'");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur Interne du serveur", detail: ex.Message);
			}

			
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterRessource ressource)
		{
			try
			{
				var validation = new RegisterValidator();
				var validationResult = await validation.ValidateAsync(ressource);
				if (!validationResult.IsValid)
				{
					_logger.LogError("Création d'un compte utilisateur : Champs obligatoires");
					return BadRequest();
				}
				User user = _mapper.Map<User>(ressource);
				var userCreated = _userService.Create(user);
				var userResult = _mapper.Map<GetUserDto>(userCreated);

				_logger.LogInformation("Création d'un compte utilisateur : Opération effectuée avec succès");
				return Ok(userResult);

			}
			catch (Exception ex)
			{
				_logger.LogError("Une erreur est survenue pendant le traitement de la requête, 'Erreur Interne du serveur'");
				return ValidationProblem(statusCode: (int)HttpCode.INTERNAL_SERVER_ERROR, title: "Erreur Interne du serveur", detail: ex.Message);
			}
		}


	}
}
