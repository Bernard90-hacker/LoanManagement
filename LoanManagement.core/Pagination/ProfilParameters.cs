namespace LoanManagement.core.Pagination
{
	public class ProfilParameters : QueryStringParameters
	{
		public string Code { get; set; } = string.Empty;
        public DateTime DateExpiration { get; set; }
		public string Statut { get; set; } = string.Empty;
        public DateTime DateAjout { get; set; }
    }
}
