namespace LoanManagement.core.Models.Loan_Management
{
	public class StatutDossierClient
	{
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;
        public bool? DecisionFinale { get; set; }
        public string Motif { get; set; } = string.Empty;
        public int EtapeDeroulementId { get; set; }
        public EtapeDeroulement? EtapeDeroulement { get; set; }
        public int DossierClientId { get; set; }
        public virtual DossierClient? Dossier { get; set; }
    }
}
