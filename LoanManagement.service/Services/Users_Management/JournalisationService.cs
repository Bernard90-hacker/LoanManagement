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
			//HttpRequest _request = _accessor.HttpContext.Request;
			//HttpResponse _response = _accessor.HttpContext.Response;
			//journal.OS = Constants.Config.ConfigConstants.GetOsName();
			//journal.Navigateur = Constants.Config.ConfigConstants.GetWebBrowserName(_browserDetector);
			//journal.IPAdress = Constants.Config.ConfigConstants.GetIpAddress(_accessor);
			//journal.Machine = Constants.Config.ConfigConstants.GetDeviceName();
			//journal.Peripherique = Constants.Config.ConfigConstants.GetWebBrowserDeviceType(_browserDetector);
			//journal.PageURL = _request.GetDisplayUrl();
			//journal.MethodeHTTP = (!_request.IsHttps).ToString();
			journal.DateOperation = DateTime.Now.ToString("dd/MM/yyyy/HH:mm:ss");
			journal.DateSysteme = DateTime.Now.ToString("dd/MM/yyyy");
			await _unitOfWork.Journaux.AddAsync(journal);
			await _unitOfWork.CommitAsync();	
			return;
		}
    }
}
