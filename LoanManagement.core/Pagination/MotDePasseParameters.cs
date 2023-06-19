using System.Threading.Tasks;

namespace LoanManagement.core.Pagination
{
	public class MotDePasseParameters : QueryStringParameters
	{
		public string OldPasswordHash { get; set; } = string.Empty;
		public string OldPasswordSalt { get; set; } = string.Empty;

        public MotDePasseParameters() {}
    }
}
