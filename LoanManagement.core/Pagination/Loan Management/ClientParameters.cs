namespace LoanManagement.core.Pagination.Loan_Management
{
	public class ClientParameters : QueryStringParameters
	{
		public int Indice { get; set; } //Doit être unique
		public string DateNaissance { get; set; } = string.Empty;
	}
}
