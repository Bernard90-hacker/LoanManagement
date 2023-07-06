namespace LoanManagement.core.Models.Loan_Management
{
	public class Client
	{
        public int Id { get; set; }
        public int Indice{ get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenoms { get; set; } = string.Empty;
        public string DateNaissance { get; set; } = string.Empty;
        public string Residence { get; set; } = string.Empty;
        public string Ville { get; set; } = string.Empty;
        public string Quartier { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
    }
}
