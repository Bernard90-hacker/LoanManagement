
namespace LoanManagement.core.Models.Users_Management
{
	public class Application
	{
        public int Id { get; set; }
        public string Logo { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Libelle { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Version { get; set; } = default!;
        public int Statut { get; set; }
        public DateTime DateAjout { get; set; } = default!;
        public DateTime DateModification { get; set; } = default!;
        public int ModuleId { get; set; } //Une application a une ou plusieurs modules
        public ICollection<Application> Modules = new List<Application>();
        public List<Menu> Menus { get; set; } = new();
    }
}
