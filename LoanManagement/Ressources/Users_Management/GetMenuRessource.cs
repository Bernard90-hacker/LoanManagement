namespace LoanManagement.API.Ressources.Users_Management
{
	public class GetMenuRessource
	{
		public string Logo { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty;
		public string Libelle { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int Statut { get; set; }
		public int Position { get; set; }
		public int MenuId { get; set; }
		public int ApplicationId { get; set; }
	}
}
