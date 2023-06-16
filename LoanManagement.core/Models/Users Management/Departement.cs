namespace LoanManagement.core.Models.Users_Management
{
	public class Departement
	{
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Libelle { get; set; } = string.Empty;
        public int DirectionId { get; set; }
        public Direction? Direction { get; set; }
        public List<Employe> Employes { get; set; } = new();
    }
}
