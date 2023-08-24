namespace LoanManagement.Client.Interne.ViewModels
{
    public class EmployeResource
    {
        public int Id { get; set; }
        public string Matricule { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenoms { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; }
        public int DepartementId { get; set; }
    }
}
