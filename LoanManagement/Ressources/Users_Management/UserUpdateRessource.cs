namespace LoanManagement.API.Ressources.Users_Management
{
	public class UserUpdateRessource
	{
        public string Username { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
