
namespace LoanManagement.core.Models.Loan_Management
{
	public class DossierClient
	{
        public int Id { get; set; }
        public string NumeroDossier { get; set; } = string.Empty;
        public double Taille { get; set; }
        public double Poids { get; set; }
        public double TensionArterielle { get; set; }
        public bool Fumeur { get; set; }
        public int NbrCigarettes { get; set; }
        public bool BuveurOccasionnel { get; set; }
        public bool BuveurRegulier { get; set; }
        public string Distractions { get; set; } = string.Empty;
        public bool EstSportif { get; set; }
        public int CategorieSport { get; set; } //1. Amateur 2. Professionnel.
        public bool EstInfirme { get; set; }
        public string NatureInfirmite { get; set; } = string.Empty;
        public string DateSurvenance { get; set; } = string.Empty;
		public int StatutMaritalId { get; set; }
        public int ClientId { get; set; }
		public Client? Client { get; set; }
		public PretAccord? PretAccord { get; set; }
        public virtual StatutMarital? StatutMarital { get; set; }
        public List<InfoSanteClient> InfoSanteClients { get; set; } = new();
        public List<StatutDossierClient> Status { get; set; } = new();



    }
}
