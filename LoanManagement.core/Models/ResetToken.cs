namespace LoanManagement.core.Models
{
	public class ResetToken
	{
		public string Email { get; set; } = default!;
		public string Token { get; set; } = default!;
    }
}
