namespace LoanManagement.API.Dto
{
	public class GetUserDto
	{
		public string Email { get; set; } = default!;
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
	}
}
