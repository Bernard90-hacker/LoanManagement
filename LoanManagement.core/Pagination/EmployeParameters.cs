namespace LoanManagement.core.Pagination
{
	public class EmployeParameters : QueryStringParameters
	{
		public string Email { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int Matricule { get; set; }

        public EmployeParameters() {}
    }
}
