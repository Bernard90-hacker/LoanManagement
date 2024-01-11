
namespace LoanManagement.core.Models.Loan_Management
{
	public class DossierClient
	{
        public int Id { get; set; }
        public string NumeroDossier { get; set; } = string.Empty;
        public double Taille { get; set; }
        public double Montant { get; set; }
        public double Poids { get; set; }
        public string TensionArterielle { get; set; }
        public bool Fumeur { get; set; }
        public int NbrCigarettes { get; set; }
        public int Buveur { get; set; } //1. Pas Du tout, 2. Occassionnellement, 3. Régulièrement
        public string Distractions { get; set; } = string.Empty;
        public bool EstSportif { get; set; }
        public int CategorieSport { get; set; } //1. Amateur 2. Professionnel.
        public bool EstInfirme { get; set; }
        public string NatureInfirmite { get; set; } = string.Empty;
        public string DateSurvenance { get; set; } = string.Empty;
		public int StatutMaritalId { get; set; }
        public bool Cloturer { get; set; }
        public string DateCloture { get; set; } = string.Empty;
        public string DateSoumission { get; set; } = string.Empty;
        public string CouvertureEmprunteur { get; set; } = string.Empty;
        public string CarteIdentite { get; set; } = string.Empty;
        public string ContratTravail { get; set; } = string.Empty;
        public string AttestationTravail { get; set; } = string.Empty;
        public string PremierBulletinSalaire { get; set; } = string.Empty;
        public string DeuxiemeBulletinSalaire { get; set; } = string.Empty;
        public string TroisiemeBulletinSalaire { get; set; } = string.Empty;
        public string FactureProFormat { get; set; } = string.Empty;
        public string EcheanceCarteIdentite { get; set; } = string.Empty;
        public bool DossierTraite { get; set; } = false;
        public string Statut { get; set; } = string.Empty;
        public int ClientId { get; set; }
		public Client? Client { get; set; }
		public PretAccord? PretAccord { get; set; }
        public virtual StatutMarital? StatutMarital { get; set; }
        public List<InfoSanteClient> InfoSanteClients { get; set; } = new();
        public List<StatutDossierClient> Status { get; set; } = new();



    }
}
