namespace LoanManagement.API.Ressources.Users_Management
{
    public class SmtpRessource
    {
        public int Id { get; set; }
        public string SmtpEmail { get; set; } = string.Empty;
        public string SmtpName { get; set; } = string.Empty;
        public string FromPassword { get; set; } = string.Empty;
        public string SmtpClient { get; set; } = string.Empty;
        public double Port { get; set; }
    }
}
