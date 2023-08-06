using LoanManagement.Client.Resources;
using Microsoft.AspNetCore.Http.Extensions;
using Shyjus.BrowserDetection;

namespace LoanManagement.Client.Services
{
    public class JournalisationService
    {
        private IBrowserDetector _browserDetector;
        private HttpContextAccessor _accessor;

        public JournalisationService(IBrowserDetector browser,
            HttpContextAccessor accessor)
        {
            _browserDetector = browser;
            _accessor = accessor;
        }

        public async Task<Journal> Journalize()
        {
            var journal = new Journal();
            HttpRequest _request = _accessor.HttpContext.Request;
            HttpResponse _response = _accessor.HttpContext.Response;
            journal.OS = ConfigConstant.GetOsName();
            journal.Navigateur = ConfigConstant.GetWebBrowserName(_browserDetector);
            journal.IPAdress = ConfigConstant.GetIpAddress(_accessor);
            journal.Machine = ConfigConstant.GetDeviceName();
            journal.Peripherique = ConfigConstant.GetWebBrowserDeviceType(_browserDetector);
            journal.PageURL = _request.GetDisplayUrl();
            journal.MethodeHTTP = (!_request.IsHttps).ToString();
            journal.DateOperation = DateTime.Now.ToString("dd/MM/yyyy/HH:mm:ss");
            journal.DateSysteme = DateTime.Now.ToString("dd/MM/yyyy");
            return journal;
        }
    }
}
