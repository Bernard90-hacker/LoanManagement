namespace LoanManagement.API.Ressources.Users_Management
{
	public class ParamMotDePasseUpdateExpiryDateRessource
	{
        public int Id { get; set; }
        public int ExpiryFrequency { get; set; } //La fréquence d'expiration des mots de passe est exprimée en mois.
    }
}
