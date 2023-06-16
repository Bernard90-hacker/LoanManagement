namespace LoanManagement.core.Models.Users_Management
{
	public class Direction
	{
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Libelle { get; set; } = string.Empty;
        public List<Departement> Departements { get; set; } = new();
    }
}
