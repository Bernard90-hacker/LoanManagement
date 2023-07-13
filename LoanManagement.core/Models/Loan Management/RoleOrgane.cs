namespace LoanManagement.core.Models.Loan_Management
{
	public class RoleOrgane
	{
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public int DureeTraitement { get; set; }
        public List<OrganeDecision> OrganeDecisions { get; set; } = new();
    }
}
