namespace LoanManagement.API.Ressources.Users_Management
{
	public class EmployeRessource
	{
		public string Nom { get; set; } = string.Empty;
		public string Prenoms { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Photo { get; set; } = string.Empty;
		public int UserId { get; set; }
		public int DepartementId { get; set; }
	}
}
