namespace LoanManagement.core.Models.Users_Management
{
	public class MotDePasse
	{
        public int Id { get; set; }
        public string OldPasswordHash { get; set; }
        public byte[] OldPasswordSalt { get; set; }
        public string DateAjout { get; set; } = string.Empty;
        public int UtilisateurId { get; set; }
        public Utilisateur? Utilisateur { get; set; }
    }
}
