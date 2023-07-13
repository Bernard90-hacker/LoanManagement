namespace LoanManagement.API.Dto
{
	public class GetCompteDto
	{
        public string NumeroCompte { get; set; } = string.Empty;
        public double Solde { get; set; }
        public int ClientId { get; set; }
    }
}
