namespace LoanManagement.API.Ressources.Loan_Management
{
    public class SaveDossierClientResource
    {
        public double Taille { get; set; }
        public double Poids { get; set; }
        public double TensionArterielle { get; set; }
        public bool Fumeur { get; set; }
        public int NbrCigarettes { get; set; }
        public bool BuveurOccasionnel { get; set; }
        public bool BuveurRegulier { get; set; }
        public string Distractions { get; set; } = string.Empty;
        public string CouvertureEmprunteur { get; set; } = string.Empty;
        public IFormFile? CarteIdentite { get; set; }
        public IFormFile? ContratTravail { get; set; }
        public IFormFile? AttestationTravail { get; set; }
        public IFormFile? PremierBulletinSalaire { get; set; }
        public IFormFile? DeuxiemeBulletinSalaire { get; set; }
        public IFormFile? TroisiemeBulletinSalaire { get; set; }
        public IFormFile? FactureProFormat { get; set; }
        public string EcheanceCarteIdentite { get; set; } = string.Empty;
        public bool EstSportif { get; set; }
        public int CategorieSport { get; set; } //1. Amateur 2. Professionnel.
        public bool EstInfirme { get; set; }
        public string NatureInfirmite { get; set; } = string.Empty;
        public string DateSurvenance { get; set; } = string.Empty;
        public int StatutMaritalId { get; set; }
        public int ClientId { get; set; }
    }
}
