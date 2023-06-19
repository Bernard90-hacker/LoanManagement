namespace LoanManagement.core.Pagination
{
	public class ApplicationParameters : QueryStringParameters
	{
		public string Code { get; set; } = string.Empty;
		public string Libelle { get; set; } = string.Empty;
		public string Version { get; set; } = string.Empty;
		public int Statut { get; set; }
	}
}
