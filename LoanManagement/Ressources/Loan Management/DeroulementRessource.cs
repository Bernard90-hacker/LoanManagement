namespace LoanManagement.API.Ressources.Loan_Management
{
	public class DeroulementRessource
	{
		public double Plancher { get; set; }
		public double Plafond { get; set; }
		public string Libelle { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int NiveauInstance { get; set; }
		public int TypePretId { get; set; }
	}
}
