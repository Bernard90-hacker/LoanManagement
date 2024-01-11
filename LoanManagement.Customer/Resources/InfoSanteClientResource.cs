namespace LoanManagement.Customer.Resources
{
    public class InfoSanteClientResource
    {
        public bool ReponseBoolenne { get; set; }
        public string ReponsePrecision { get; set; } = string.Empty;
        public int? DureeTraitement { get; set; }
        public string PeriodeTraitement { get; set; } = string.Empty;
        public string LieuTraitement { get; set; } = string.Empty;
        public int NatureQuestionId { get; set; }
        public int DossierClientId { get; set; }
    }
}
