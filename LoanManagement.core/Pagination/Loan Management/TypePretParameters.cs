namespace LoanManagement.core.Pagination.Loan_Management
{
	public class TypePretParameters : QueryStringParameters
	{
		public string Libelle { get; set; } = string.Empty;
		public int Duree { get; set; } //La durée est exprimée en années
	}
}
