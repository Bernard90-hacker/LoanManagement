
namespace LoanManagement.core.Models.Users_Management
{
	public class Application
	{
        public int Id { get; set; }
        public string Logo { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Libelle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public int Statut { get; set; }
        public string DateAjout { get; set; } = string.Empty;
        public string DateModification { get; set; } = string.Empty;
        public int? ApplicationId { get; set; } //Cet attribut veut dire que cet objet est un sous-module qui référence un module existant.
        public List<Menu> Menus { get; set; } = new();
	}
}
