namespace LoanManagement.API.Ressources.Users_Management
{
	public class UpdateProfilExpiryDateRessource
	{
        public int ProfilId { get; set; }
        public string DateExpiration { get; set; } = string.Empty;
    }
}
