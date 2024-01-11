﻿using LoanManagement.core.Models.Loan_Management;
using Microsoft.AspNetCore.Http;

namespace LoanManagement.core.Models.Users_Management
{
	public class Employe
	{
        public int Id { get; set; }
        public string Matricule { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenoms { get; set; } =string.Empty;
        public string NomComplet { get; set; } = string.Empty;
        public string? Photo { get; set; } = string.Empty;
        public string DateAjout { get; set; } = string.Empty;
        public string DateModification { get; set; } = string.Empty;
        public int UserId { get; set; }
        public Utilisateur? User { get; set; }
        public int DepartementId { get; set; }
        public Departement? Departement { get; set; }

        public Employe()
        {
            NomComplet = $"{Nom} {Prenoms}";
        }
    }
}
