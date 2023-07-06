using Azure.Core;
using LoanManagement.core.Models.Users_Management;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Data.SqlClient;
using Shyjus.BrowserDetection;

namespace LoanManagement.service.Services.Users_Management
{
	public class JournalisationService
	{
		private IUnitOfWork _unitOfWork;
		private IBrowserDetector _browserDetector;
		private HttpContextAccessor _accessor;

        public JournalisationService(IUnitOfWork unitOfWork, IBrowserDetector browser, 
			HttpContextAccessor accessor)
        {
			_unitOfWork = unitOfWork;
			_browserDetector = browser;
			_accessor = accessor;
        }

		public async Task Journalize(Journal journal)
		{
			using (SqlConnection connection = new SqlConnection
				("Server=DESKTOP-NPL8P5C\\SQLEXPRESS;Database=SoutenanceDb;" +
				"Trusted_Connection=True;MultipleActiveResultSets=true; " +
				"TrustServerCertificate=true;"))
			{
				connection.Open();

				using (SqlTransaction transaction = connection.BeginTransaction())
				{
					try
					{
						HttpRequest _request = _accessor.HttpContext.Request;
						HttpResponse _response = _accessor.HttpContext.Response;
						journal.OS = Constants.Config.ConfigConstants.GetOsName();
						journal.Navigateur = Constants.Config.ConfigConstants.GetWebBrowserName(_browserDetector);
						journal.IPAdress = Constants.Config.ConfigConstants.GetIpAddress(_accessor);
						journal.Machine = Constants.Config.ConfigConstants.GetDeviceName();
						journal.Peripherique = Constants.Config.ConfigConstants.GetWebBrowserDeviceType(_browserDetector);
						journal.PageURL = _request.GetDisplayUrl();
						journal.MethodeHTTP = (!_request.IsHttps).ToString();
						journal.DateOperation = DateTime.Now.ToString("dd/MM/yyyy/HH:mm:ss");
						journal.DateSysteme = DateTime.Now.ToString("dd/MM/yyyy");

						switch (journal.PageURL)
						{
							case "https://localhost:44304/api/users/Auth/Login":
								journal.TypeJournalId = 1;
								journal.Niveau = 2; //2 est synonyme de succès
								await _unitOfWork.Journaux.AddAsync(journal);
								transaction.Commit();
								return ;
							case "https://localhost:44304/api/users/Auth/Logout":
								journal.TypeJournalId = 2;
								await _unitOfWork.Journaux.AddAsync(journal);
								transaction.Commit();
								return;
							default:
								break;
						}
						if (_request.Method == "POST")
							journal.TypeJournalId = 5;
						if (_request.Method == "PUT")
							journal.TypeJournalId = 3;
						if (_request.Method == "DELETE")
							journal.TypeJournalId = 4;
						if (_request.Method == "GET" && journal.PageURL.Contains("id"))
							journal.TypeJournalId = 6;
						if (_request.Method == "GET" && journal.PageURL.Contains("code"))
							journal.TypeJournalId = 7;
						if (_request.Method == "GET")
							journal.TypeJournalId = 8;
						journal.Niveau = 2;
						await _unitOfWork.Journaux.AddAsync(journal);

						transaction.Commit();
					}
					catch (Exception ex)
					{
						transaction.Rollback();
					}
				}
			}
		}
    }
}
