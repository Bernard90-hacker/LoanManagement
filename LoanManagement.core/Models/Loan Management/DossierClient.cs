﻿
namespace LoanManagement.core.Models.Loan_Management
{
	public class DossierClient
	{
        public int Id { get; set; }
        public string NumeroDossier { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenoms { get; set; } = string.Empty;
        public int StatutMaritalId { get; set; }
        public StatutMarital? StatutMarital { get; set; }
        public string Profession { get; set; } = string.Empty;
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

    }
}
