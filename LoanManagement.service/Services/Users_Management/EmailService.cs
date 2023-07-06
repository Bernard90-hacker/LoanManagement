using System.Net.Mail;
using System.Net;
namespace LoanManagement.service.Services.Users_Management
{
	public class EmailService
	{
        public EmailService() {}
        private static string smtpEmail = "netatlas90@gmail.com";
		private static string smtpName = "Kokou Venunye Bernard SOGBO";
		private static string fromPassword = "yogjdetlcgtsaqrv";
		public void SendPasswordResetMailAsync(string password, string email)
		{
			SmtpClient client = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential(smtpEmail, fromPassword),
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
