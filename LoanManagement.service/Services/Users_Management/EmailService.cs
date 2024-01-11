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
        public async Task SendPasswordMailAsync(string password, string username, string email)
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
                Subject = "Vos identifiants : ",
                Body = $"Vos identifiants sont: <html <body> Nom d'utilisateur: {username}  Mot de passe {password}</body> </html>",
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(email));
            client.Send(message);
            client.Dispose();

        }
		public async Task NotifyClient(bool accept, string email, double montant)
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

			MailMessage mail = new MailMessage()
			{
				From = new MailAddress(smtpEmail, smtpName),
				Subject = "Vos identifiants : ",
				IsBodyHtml = true
			};
			if (accept) mail.Body = $"<html <body> Votre demande de crédit s'élevant à {montant} CFA a été accordée </body> </html>";
			else mail.Body = $"<html <body> Votre demande de crédit s'élevant à {montant} CFA a rejetée </body> </html>";
			mail.To.Add(new MailAddress(email));
			client.Send(mail);
			client.Dispose();

		}
	}
}
