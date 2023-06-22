namespace LoanManagement.API.Ressources.Users_Management
{
	public class UpdateUserAccountExpiryDateRessource
	{
        public int UserId { get; set; }
        public string ExpiryDate { get; set; } = string.Empty;
    }
}
