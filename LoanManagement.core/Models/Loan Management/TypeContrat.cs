﻿namespace LoanManagement.core.Models.Loan_Management
{
	public class TypeContrat
	{
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Libelle { get; set; } = string.Empty;
        public List<PretAccord>? Prets { get; set; }
    }
}
