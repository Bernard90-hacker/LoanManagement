namespace LoanManagement.API.Ressources.Users_Management
{
	public class UtilisateurRessource 
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public bool IsEditPassword { get; set; }
		public bool IsSuperAdmin { get; set; }
		public bool IsAdmin { get; set; }
		public string DateExpirationCompte { get; set; } = string.Empty;
		public int Statut { get; set; } //1 = Actif, 2 = Non Actif
        public int ProfilId { get; set; }
        public string DateDesactivation { get; set; } = string.Empty;
	}
}
