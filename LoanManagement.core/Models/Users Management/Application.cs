
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
        public DateTime DateAjout { get; set; }
        public DateTime DateModification { get; set; }
        public int ModuleId { get; set; }
        public Application? Module { get; set; }
        public ICollection<Application> Modules = new List<Application>();
        public List<Menu> Menus { get; set; } = new();
    }
}
