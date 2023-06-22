namespace LoanManagement.API.Ressources.Users_Management
{
	public class MotDePasseRessource
	{
		public string OldPasswordHash { get; set; } = string.Empty;
		public byte[] OldPasswordSalt { get; set; }
		public string DateAjout { get; set; } = string.Empty;
		public int UtilisateurId { get; set; }
	}
}
