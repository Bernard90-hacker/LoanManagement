namespace LoanManagement.core.Pagination.Loan_Management
{
	public class CompteParameters : QueryStringParameters
	{
		public string NumeroCompte { get; set; } = string.Empty; //Doit être unique
		public int ClientId { get; set; }
	}
}
