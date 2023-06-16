namespace LoanManagement.core.Pagination
{
	public class TypeJournalParameters : QueryStringParameters
	{
		public string Code { get; set; } = string.Empty;
        public int Statut { get; set; }
    }
}
