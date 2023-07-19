namespace LoanManagement.core.Models.Loan_Management
{
	public class OrganeDecision
	{
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public List<MembreOrgane> Membres { get; set; } = new();
        public List<RoleOrgane> Roles { get; set; } = new();
    }
}
