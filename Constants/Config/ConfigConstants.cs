using Constants.Utils;
using Microsoft.Extensions.Hosting;

namespace Constants.Config
{
	public static class ConfigConstants
	{

		public static string GetLoanManagementApiUrlBase(IConfiguration config)
		{
			return config.GetSection("BaseURL").GetSection("WebTaskApiURL").Value;
		}

		public static string IsMenuActive(this IHtmlHelper html, string areas, string controllers, string actions,
		   string menuClass = "active")
		{
			var currentArea = (string)html.ViewContext.RouteData.Values["area"];
			var currentController = (string)html.ViewContext.RouteData.Values["controller"];
			var currentAction = (string)html.ViewContext.RouteData.Values["action"];

			IEnumerable<string> acceptedAreas = areas.Split(',');
			IEnumerable<string> acceptedControllers = controllers.Split(',');
			IEnumerable<string> acceptedActions = actions.Split(',');

			return (from area in acceptedAreas
					from controller in acceptedControllers
					from action in acceptedActions
					where area.Trim().Equals(currentArea)
						  && controller.Trim().Equals(currentController)
						  && action.Trim().Equals(currentAction)
					select area).Any()
				? menuClass : string.Empty;
		}


		public static void AddToCache(IMemoryCache memoryCache, string key, object obj)
		{
			var option = new MemoryCacheEntryOptions
			{
				SlidingExpiration = TimeSpan.FromSeconds(15),
				AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
			};

			memoryCache.Set(key, obj, option);
		}


		public static string GetOsName()
		{
			string osName;
			try
			{
				var os = Environment.OSVersion;
				osName = os.VersionString;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return osName;
		}

		public static string GetDeviceName()
		{
			string deviceName;
			try
			{
				deviceName = Dns.GetHostName();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return deviceName;
		}

		public static string GetIpAddress(HttpContextAccessor accessor)
		{
			string ipAddress = string.Empty;
			try
			{
				if (accessor.HttpContext != null)
				{
					IPAddress remoteIpAddress = accessor.HttpContext.Connection.RemoteIpAddress;
					if (remoteIpAddress != null)
					{
						if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
							remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList
								.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
						ipAddress = remoteIpAddress.ToString();
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return ipAddress;
		}

		public static string GetWebBrowserDeviceType(IBrowserDetector browserDetector)
		{
			string webBrowserDeviceType = string.Empty;
			try
			{
				webBrowserDeviceType = browserDetector.Browser.DeviceType;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return webBrowserDeviceType;
		}

		public static string GetWebBrowserName(IBrowserDetector browserDetector)
		{
			string webBrowserName = string.Empty;
			try
			{
				webBrowserName = $"{browserDetector.Browser.Name} {browserDetector.Browser.Version}";
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return webBrowserName;
		}

		public static string? GetCurrentRouteFromSession(HttpContext context)
		{
			string? route = null;

			var area = context.Session.GetString("area");
			var controller = context.Session.GetString("controller");
			var action = context.Session.GetString("action");
			var id = context.Session.GetString("idParam");

			if (id.Any() && action.Any() && controller.Any() && area.Any())
				return null;

			if (id.Any() && !action.Any() && !controller.Any() && area.Any())
				route = $"{null},{controller},{action},{null}";

			if (!id.Any() && !action.Any() && !controller.Any() && area.Any())
				route = $"{null},{controller},{action},{id}";

			if (id.Any() && !action.Any() && !controller.Any() && !area.Any())
				route = $"{area},{controller},{action},{null}";

			if (!id.Any() && !action.Any() && !controller.Any() && !area.Any())
				route = $"{area},{controller},{action},{id}";

			return route;
		}

		public static void DeleteCurrentRouteFromSession(HttpContext context)
		{
			context.Session.Remove("area");
			context.Session.Remove("controller");
			context.Session.Remove("action");
			context.Session.Remove("idParam");
		}

		public static void AddCookie(HttpResponse response, string key, string value)
		{
			CookieOptions cookieOptions = new();
			response.Cookies.Append(key, value, cookieOptions);
		}

		public static string GetCookie(HttpRequest request, string key)
		{
			return request.Cookies[key];
		}

		public static void DeleteCookie(HttpResponse response, string key)
		{
			response.Cookies.Delete(key);
		}

		public static bool IsNetworkAvailable()
		{
			return NetworkInterface.GetIsNetworkAvailable();
		}

        public static async Task<string?> UploadApiFile(IHostEnvironment env, IFormFile file, string path)
        {
            if (file is not { Length: > 0 }) return null;

            string nomFichier;
            try
            {
                var extension = Path.GetExtension(file.FileName);
                if (!extension.ToLower().In(".png", ".jpg", ".jpeg"))
                    return "NotAccepted";
                nomFichier = UtilsConstant.RandomString(6) + DateTime.Now.ToString("yyyymmddssfff") + extension;
                var repertoireServeur = Path.Combine(env.ContentRootPath, path);
                if (!Directory.Exists(repertoireServeur)) Directory.CreateDirectory(repertoireServeur);
                using FileStream fileStream = new(Path.Combine(repertoireServeur, nomFichier), FileMode.OpenOrCreate);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return nomFichier;
        }

        public static async Task<string?> UploadPdfFile(IHostEnvironment env, IFormFile? file, string path, string nomClient, int i)
        {
            if (file is not { Length: > 0 }) return null;

            string nomFichier;
            try
            {
                var extension = Path.GetExtension(file.FileName);
                if (!extension.ToLower().In(".pdf"))
                    return "NotAccepted";
				switch (i)
				{
					case 1: //Attestation de travail
						nomFichier = $"{nomClient}_ATTESTATION_TRAVAIL";
						break;
					case 2: //CONTRAT TRAVAIL
						nomFichier = $"{nomClient}_CONTRAT_TRAVAIL";
						break;
					case 3: //PREMIER_BULLETIN_SALAIRE
						nomFichier = $"{ nomClient}_PREMIER_BULLETIN_SALAIRE";
						break;
					case 4: //DEUXIEME BULLETIN SALAIRE
						nomFichier = $"{nomClient}_DEUXIEME_BULLETIN_SALAIRE";
						break;
					case 5: //TROISIEME_BULLETIN_SALAIRE
						nomFichier = $"{nomClient}_TROISIEME_BULLETIN_SALAIRE";
						break;
					case 6: //FACTURE PROFORMAT
						nomFichier = $"{nomClient}_FACTURE_PROFORMA";
						break;
					case 7: //CARTE_IDENTITE
						nomFichier = $"{nomClient}_CARTE_IDENTITE";
						break;
					default:
						break;
				}
				nomFichier = UtilsConstant.RandomString(6) + DateTime.Now.ToString("yyyymmddssfff") + extension;
                var repertoireServeur = Path.Combine(env.ContentRootPath, path);
                if (!Directory.Exists(repertoireServeur)) Directory.CreateDirectory(repertoireServeur);
                using FileStream fileStream = new(Path.Combine(repertoireServeur, nomFichier), FileMode.OpenOrCreate);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return nomFichier;
        }
    }
}
