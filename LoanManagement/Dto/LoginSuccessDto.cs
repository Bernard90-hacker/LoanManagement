namespace LoanManagement.API.Dto
{
	public class LoginSuccessDto
	{
		public string Email { get; set; } = string.Empty;
		public string Nom { get; set; } = string.Empty;
		public string Prenoms { get; set; } = string.Empty;
		public string CodeProfil { get; set; } = string.Empty;
		public string DateExpirationProfil { get; set; } = string.Empty;
		public string CodeDepartement { get; set; } = string.Empty;
		public string Direction { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
    }
}
