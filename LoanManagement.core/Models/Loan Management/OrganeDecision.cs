namespace LoanManagement.core.Models.Loan_Management
{
	public class OrganeDecision
	{
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public int RoleOrganeId { get; set; }
        public RoleOrgane? Role { get; set; }
        public List<MembreOrgane> Membres { get; set; } = new();
    }
}
