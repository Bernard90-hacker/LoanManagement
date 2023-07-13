namespace LoanManagement.core.Models.Loan_Management
{
	public class TypePret
	{
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public List<Deroulement> Deroulements { get; set; } = new();
        public List<PretAccord> PretAccords { get; set; } = new();

    }
}
