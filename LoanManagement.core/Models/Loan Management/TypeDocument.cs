namespace LoanManagement.core.Models.Loan_Management
{
	public class TypeDocument
	{
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Libelle { get; set; } = string.Empty;
        public int DossierClientId { get; set; }
        public DossierClient? DossierClient { get; set; }
    }
}
