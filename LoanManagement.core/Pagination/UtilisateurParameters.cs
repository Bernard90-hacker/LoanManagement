namespace LoanManagement.core.Pagination
{
	public class UtilisateurParameters : QueryStringParameters
	{
		public string Username { get; set; } = string.Empty;
        public bool IsConnected { get; set; }
		public DateTime DateExpirationCompte { get; set; }
		public int Statut { get; set; }
		public DateTime DateDesactivation { get; set; }
		public DateTime DateAjout { get; set; }
		public DateTime DateModificationMotDePasse { get; set; }
	}
}
