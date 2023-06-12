namespace LoanManagement.API.Ressources
{
	public class LoginRessource
	{
		[JsonPropertyName("email")]
		public string Email { get; set; } = default!;
		[JsonPropertyName("password")]
		public string Password { get; set; } = default!;
	}
}
