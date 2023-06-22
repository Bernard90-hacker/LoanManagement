namespace LoanManagement.core.Models.Users_Management
{
	public class Journal
	{
        public int Id { get; set; }
        public int Niveau { get; set; } //ECHEC, SUCCES, INFORMATION
        public string Libelle { get; set; } = string.Empty;
        public string Machine { get; set; } = string.Empty;
        public string Peripherique { get; set; } = string.Empty;
        public string OS { get; set; } = string.Empty;
        public string Navigateur { get; set; } = string.Empty;
        public string IPAdress { get; set; } = string.Empty;
        public string MethodeHTTP { get; set; } = string.Empty;
        public string Entite { get; set; } = string.Empty;
        public string PageURL { get; set;} = string.Empty;
        public string PreferenceURL { get; set;} = string.Empty;
        public string DateOperation { get; set; } = string.Empty;
        public string DateSysteme { get; set; } = string.Empty;
        public int UtilisateurId { get; set; }
        public Utilisateur? Utilisateur { get; set; }
        public int TypeJournalId { get; set; }
        public TypeJournal? TypeJournal { get; set; }
    }
}
