
namespace LoanManagement.core.Models.Loan_Management
{
	public class DossierClient
	{
        public int Id { get; set; }
        public string NumeroDossier { get; set; } = string.Empty;
        public int EmployeurId { get; set; }
        public Employeur? Employeur { get; set; }
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
        public int StatutDossierClientId { get; set; }
        public StatutDossierClient? StatutDossierClient { get; set; }
        public int InfoSanteClientId { get; set; }
        public virtual InfoSanteClient? InfoSanteClient { get; set; }
		public int StatutMaritalId { get; set; }
        public int ClientId { get; set; }
		public int PretAccordId { get; set; }
		public PretAccord? PretAccord { get; set; }
		public virtual Client Client { get; set; } = new();
        public virtual StatutMarital? StatutMarital { get; set; }



    }
}
