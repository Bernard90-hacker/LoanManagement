namespace LoanManagement.core.Models.Loan_Management
{
	public class EtapeDeroulement
	{
        public int Id { get; set; }
        public int Etape { get; set; }
        public int DeroulementId { get; set; }
        public int MembreOrganeId { get; set; }
        public virtual MembreOrgane? MembreOrgane { get; set; }
        public virtual Deroulement? Deroulement { get; set; }
    }
}
