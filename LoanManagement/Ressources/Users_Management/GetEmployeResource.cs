namespace LoanManagement.API.Ressources.Users_Management
{
    public class GetEmployeResource
    {
        public string Nom { get; set; } = string.Empty;
        public string Prenoms { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Matricule { get; set; }
        public string Photo { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int DepartementId { get; set; }

    }
}
