using System.Net.Mail;
using System.Net;
namespace LoanManagement.service.Services.Users_Management
{
	public class EmailService
	{
		private readonly IUnitOfWork _unitOfWork;
		public EmailService(IUnitOfWork unitOfWork)
			=> _unitOfWork = unitOfWork;

        public async Task<string> GetSmtpEmail()
		{
			var current = await _unitOfWork.ParamMotDePasses.GetCurrentParameter();
			return current.SmtpEmail;
		}
        public async Task<string> GetSmtpName()
        {
            var current = await _unitOfWork.ParamMotDePasses.GetCurrentParameter();
            return current.SmtpName;
        }
		public async Task<int> GetSmtpPort()
		{
			var current = await _unitOfWork.ParamMotDePasses.GetCurrentParameter();
			return current.Port;
		}
		public async Task<string> FromPwd()
		{
			var current = await _unitOfWork.ParamMotDePasses.GetCurrentParameter();
			return current.FromPassword;
		}
        public async Task<string> GetSmtpClient()
        {
            var current = await _unitOfWork.ParamMotDePasses.GetCurrentParameter();
            return current.SmtpClient;
        }
       
		public async Task SendPasswordResetMailAsync(string password, string email)
		{
			var smtpClient = await GetSmtpClient();
			var port = await GetSmtpPort();
			var smtpEmail = await GetSmtpEmail();
			var smtpName = await GetSmtpName();
			var from = await FromPwd();
			SmtpClient client = new SmtpClient(smtpClient)
			{
				Port = port,
				Credentials = new NetworkCredential(smtpEmail, from),
				EnableSsl = true
			};

			MailMessage message = new MailMessage()
			{
				From = new MailAddress(smtpEmail, smtpName),
				Subject = "Forgot your password",
				Body = $"Votre nouveau mot de passe : <html <body> {password}  </body> </html>",
				IsBodyHtml = true
			};
			message.To.Add(new MailAddress(email));
			client.Send(message);
			client.Dispose();

		}
	}
}
