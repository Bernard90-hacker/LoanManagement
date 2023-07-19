namespace LoanManagement.API.Ressources.Loan_Management
{
	public class RoleOrganeRessource
	{
		public string Libelle { get; set; } = string.Empty;
        public int OrganeDecisionId { get; set; }
        public int DureeTraitement { get; set; }
	}
}
