using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LoanManagement.core.Dto
{
	public class UserUpdatedDto
	{
		[JsonPropertyName("email")]
		public string Email { get; set; } = default!;
		[JsonPropertyName("first_name")]
		public string FirstName { get; set; } = default!;
		[JsonPropertyName("last_name")]
		public string LastName { get; set; } = default!;
    }
}
