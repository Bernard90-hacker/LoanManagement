namespace LoanManagement.core.Models.Loan_Management
{
	public class Employeur
	{
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string BoitePostale { get; set; } = string.Empty; //Unique
        public string Tel { get; set; } = string.Empty; //Unique

        public List<DossierClient>? DossierClients { get; set; }
    }

}
