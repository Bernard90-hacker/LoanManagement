namespace LoanManagement.Customer.Resources
{
    public class ClientResource
    {
        public int Id { get; set; }
        public int Indice { get; set; } //Doit être unique
        public string Nom { get; set; } = string.Empty;
        public string Prenoms { get; set; } = string.Empty;
        public string DateNaissance { get; set; } = string.Empty;
        public string LieuNaissance { get; set; } = string.Empty;
        public string AdressePostale { get; set; } = string.Empty;
        public string Residence { get; set; } = string.Empty;
        public string Ville { get; set; } = string.Empty;
        public string Quartier { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
    }
}
