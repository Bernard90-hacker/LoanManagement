namespace LoanManagement.API.Ressources.Loan_Management
{
	public class UpdateEtapeDeroulementRessource
	{
        public int Id { get; set; }
		public int Etape { get; set; }
		public int DeroulementId { get; set; }
		public int MembreOrganeId { get; set; }
	}
}
