namespace LoanManagement.core.Pagination
{
	public class UserTokenParameters : QueryStringParameters
	{
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
