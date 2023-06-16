namespace LoanManagement.core.Pagination
{
	public class MenuParameters : QueryStringParameters
	{
        public int Position { get; set; }
        public int ApplicationId { get; set; }
        public string Code { get; set; } = string.Empty;
    }
}
