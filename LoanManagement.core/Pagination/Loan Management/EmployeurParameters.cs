namespace LoanManagement.core.Pagination.Loan_Management
{
	public class EmployeurParameters : QueryStringParameters
	{
		public string BoitePostale { get; set; } = string.Empty;
		public string Tel { get; set; } = string.Empty;
	}
}
