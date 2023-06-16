namespace LoanManagement.core.Models.Users_Management
{
	public class MotDePasse
	{
        public int Id { get; set; }
        public string OldPasswordHash { get; set; } = string.Empty;
        public string OldPasswordSalt { get; set; } = string.Empty;
        public DateTime DateAjout { get; set; }
        public int UtilisateurId { get; set; }
        public Utilisateur? Utilisateur { get; set; }
    }
}
