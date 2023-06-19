namespace LoanManagement.core.Pagination
{
	public class JournalParameters : QueryStringParameters
	{
		public string IPAdress { get; set; } = string.Empty;
		public string MethodeHTTP { get; set; } = string.Empty;
		public string Entite { get; set; } = string.Empty;
		public DateTime DateOperation { get; set; }
	}
}
