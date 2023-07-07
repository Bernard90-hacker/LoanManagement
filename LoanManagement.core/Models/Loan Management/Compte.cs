namespace LoanManagement.core.Models.Loan_Management
{
	public class Compte
	{
        public int Id { get; set; }
        public string NumeroCompte { get; set; } = string.Empty; //Doit être unique
        public double Solde { get; set; }
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }
    }
}
