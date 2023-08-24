namespace LoanManagement.API.Ressources.Users_Management
{
	public class ParamGlobalRessource
	{
		public bool IncludeDigits { get; set; }
		public bool IncludeLowerCase { get; set; }
		public bool IncludeUpperCase { get; set; }
		public bool IncludeSpecialCharacters { get; set; }
		public bool ExcludeUsername { get; set; }
		public int Taille { get; set; }
		public int DelaiExpiration { get; set; } //Mois
        public string SmtpEmail { get; set; } = string.Empty;
        public string SmtpName { get; set; } = string.Empty;
        public string FromPassword { get; set; } = string.Empty;
        public string SmtpClient { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
