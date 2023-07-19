namespace LoanManagement.API.Ressources.Loan_Management
{
	public class UpdateOrganeDecisionRessource
	{
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
		public int RoleOrganeId { get; set; }
	}
}
