namespace LoanManagement.core.Models.Loan_Management
{
	public class StatutMarital
	{
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
		public List<DossierClient>? Dossiers { get; set; }
	}
}
