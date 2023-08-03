    namespace LoanManagement.core.Models.Users_Management
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string RefreshTokenTime { get; set; } = string.Empty;
        public bool IsEditPassword { get; set; }
        public bool IsConnected { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsAdmin { get; set; }
        public string DateExpirationCompte { get; set; } = string.Empty;
        public int Statut { get; set; }
        public string DateDesactivation { get; set; } = string.Empty;
        public string DateAjout { get; set; } = string.Empty;
        public string DateModificationMotDePasse { get; set; } = string.Empty;
        public int? ProfilId { get; set; }
		public Profil? Profil { get; set; }
		public Employe? Employe { get; set; }
        public List<MotDePasse> Passwords { get; set; } = new();
        public List<MembreOrgane> Membres { get; set; } = new();
    }
}
