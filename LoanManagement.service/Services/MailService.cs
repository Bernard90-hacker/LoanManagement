using LoanManagement.core.Models;
using System.Net.Mail;

namespace LoanManagement.service.Services
{
	public class MailService
	{
		private static string smtpClient = "localhost";
		private static int smtpPort = 1025;
		private static string smtpEmail = "netatlas90@gmail.com";
		private static string smtpName = "Kokou Venunye Bernard SOGBO";


		public MailService() { }

		public static async void SendPasswordResetMailAsync(ResetToken token)
		{
			SmtpClient client = new SmtpClient()
			{
				Host = "localhost",
				Port = smtpPort,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = true,
				EnableSsl = false
			};

			MailMessage message = new MailMessage()
			{
				From = new MailAddress(smtpEmail, smtpName),
				Subject = "Reset your password",
				Body = $"Click <a href=\"http://localhost:4200/api/reset/{token.Token}\"> here </a> to reset password",
				IsBodyHtml = true
			};

			message.To.Add(new MailAddress(token.Email));
			await client.SendMailAsync(message);
			message.Dispose();
		}

	}
}
