namespace LoanManagement.core.Models.Users_Management
{
	public class TypeJournal
	{
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Libelle { get; set; } = string.Empty;
        public int Statut { get; set; }

        public List<Journal> Journaux { get; set; } = new();
    }
}
