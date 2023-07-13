namespace LoanManagement.core.Models.Loan_Management
{
	public class Deroulement
	{
        public int Id { get; set; }
        public double Plancher { get; set; }
        public double Plafond { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NiveauInstance { get; set; }
        public int TypePretId { get; set; }
        public TypePret? TypePret { get; set; }
        public List<EtapeDeroulement> Etapes { get; set; } = new();
    }
}
