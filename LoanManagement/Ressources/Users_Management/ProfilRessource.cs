namespace LoanManagement.API.Ressources.Users_Management
{
	public class ProfilRessource
	{
		public string Libelle { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DateExpiration { get; set; } = string.Empty;
		public int Statut { get; set; }
    }
}
