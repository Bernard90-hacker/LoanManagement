namespace LoanManagement.core.Pagination.Loan_Management
{
	public class DossierClientParameters : QueryStringParameters
	{
		public string NumeroDossier { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public int StatutMarital { get; set; }
    }
}
