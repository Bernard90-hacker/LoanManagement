namespace LoanManagement.API.Validator.Users_Management
{
	public class GetUtilisateurRessource
	{
		public string Username { get; set; } = string.Empty;
		public string DateExpirationCompte { get; set; } = string.Empty;
		public int Statut { get; set; }
		public string DateDesactivation { get; set; } = string.Empty;
		public string DateAjout { get; set; } = string.Empty;
		public string DateModificationMotDePasse { get; set; } = string.Empty;

	}
}
