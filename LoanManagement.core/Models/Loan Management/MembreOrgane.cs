
namespace LoanManagement.core.Models.Loan_Management
{
	public class MembreOrgane
	{
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public int OrganeDecisionId { get; set; }
        public OrganeDecision? OrganeDecision { get; set; }
        public int UtilisateurId { get; set; }
        public Utilisateur? Utilisateur { get; set; }
    }
}
