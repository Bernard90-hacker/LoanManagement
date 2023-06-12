using System.Text.Json.Serialization;

namespace LoanManagement.API.Ressources
{
	public class UserRessource
	{
		public int Id { get; set; }
		[JsonPropertyName("email")]
		public string Email { get; set; } = default!;
		[JsonPropertyName("first_name")]
		public string FirstName { get; set; } = default!;
		[JsonPropertyName("last_name")]
		public string LastName { get; set; } = default!;
		[JsonPropertyName("password")]
		[System.Text.Json.Serialization.JsonIgnore]
		public string Password { get; set; } = default!;
	}
}
