namespace LoanManagement.Client.Resources
{
    public class Journal
    {
        public int Niveau { get; set; } //1.ECHEC, 2.SUCCES, 3.INFORMATION
        public string Libelle { get; set; } = string.Empty;
        public string Machine { get; set; } = string.Empty;
        public string Peripherique { get; set; } = string.Empty;
        public string OS { get; set; } = string.Empty;
        public string Navigateur { get; set; } = string.Empty;
        public string IPAdress { get; set; } = string.Empty;
        public string MethodeHTTP { get; set; } = string.Empty;
        public string Entite { get; set; } = string.Empty;
        public string PageURL { get; set; } = string.Empty;
        public string PreferenceURL { get; set; } = string.Empty;
        public string DateOperation { get; set; } = string.Empty;
        public string DateSysteme { get; set; } = string.Empty;
        public int UtilisateurId { get; set; }
        public int ClientId { get; set; }
        public int TypeJournalId { get; set; }
    }
}
