namespace LoanManagement.API.Ressources.Users_Management
{
    public class UpdateApplicationStatusRessource
    {
        public string ApplicationCode { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
