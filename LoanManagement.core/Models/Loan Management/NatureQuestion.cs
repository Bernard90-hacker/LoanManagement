namespace LoanManagement.core.Models.Loan_Management
{
	public class NatureQuestion
	{
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public List<InfoSanteClient> InfoSanteClient { get; set; } = new();

    }
}
