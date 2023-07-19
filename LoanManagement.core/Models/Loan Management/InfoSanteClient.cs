namespace LoanManagement.core.Models.Loan_Management
{
	public class InfoSanteClient
	{
        public int Id { get; set; }
        public bool ReponseBoolenne { get; set; }
        public string ReponsePrecision { get; set; } = string.Empty;
        public int DureeTraitement { get; set; }
        public string PeriodeTraitement { get; set; } = string.Empty;
        public string LieuTraitement { get; set; } = string.Empty;
        public int NatureQuestionId { get; set; }
        public NatureQuestion? NatureQuestion { get; set; }
        public int DossierClientId { get; set; }
        public DossierClient? Dossier { get; set; }

    }
}
