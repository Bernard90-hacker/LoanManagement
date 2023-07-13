namespace LoanManagement.core.Models.Loan_Management
{
	public class PeriodicitePaiement
	{
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public List<PretAccord> PretAccords { get; set; } = new();
    }
}
