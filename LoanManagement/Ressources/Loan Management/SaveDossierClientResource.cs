﻿namespace LoanManagement.API.Ressources.Loan_Management
{
    public class SaveDossierClientResource
    {
        public int Id { get; set; }
        public double Taille { get; set; }
        public double Poids { get; set; }
        public double Montant { get; set; }
        public string TensionArterielle { get; set; } = string.Empty;
        public bool Fumeur { get; set; }
        public int NbrCigarettes { get; set; }
        public int Buveur { get; set; } //1. Pas Du tout, 2. Occassionnellement, 3. Régulièrement
        public string Distractions { get; set; } = string.Empty;
        public string CouvertureEmprunteur { get; set; } = string.Empty;
        public string? CarteIdentite { get; set; }
        public string? ContratTravail { get; set; }
        public string? AttestationTravail { get; set; }
        public string? PremierBulletinSalaire { get; set; }
        public string? DeuxiemeBulletinSalaire { get; set; }
        public string? TroisiemeBulletinSalaire { get; set; }
        public string? FactureProFormat { get; set; }
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
