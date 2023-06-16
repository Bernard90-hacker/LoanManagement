namespace LoanManagement.core.Models.Users_Management
{
	public class Utilisateur
	{
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public int RefreshTokenTime { get; set; } //Jours
        public bool IsEditPassword { get; set; }
        public bool IsConnected { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime DateExpirationCompte { get; set; }
        public int Statut { get; set; }
        public DateTime DateDesactivation { get; set; }
        public DateTime DateAjout { get; set; }
        public DateTime DateModificationMotDePasse { get; set; }
        public List<MotDePasse> Passwords { get; set; } = new();
        public List<Journal> Journaux { get; set; } = new();
    }
}
