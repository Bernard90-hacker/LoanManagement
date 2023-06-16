namespace LoanManagement.API.Ressources
{
	public class RegisterRessource
	{
		[JsonPropertyName("first_name")]
		public string FirstName { get; set; } 
		[JsonPropertyName("last_name")]
		public string LastName { get; set; }
		[JsonPropertyName("email")]
		public string Email { get; set; }
		[JsonPropertyName("password")]
		public string Password { get; set; }
		[JsonPropertyName("password_confirm")]
		public string PasswordConfirm { get; set; }
    }
}
