namespace LoanManagement.core.Models.Users_Management
{
	public class Menu
	{
        public int Id { get; set; }
        public string Logo { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Libelle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Statut { get; set; }
        public int Position { get; set; }
        public DateTime DateAjout { get; set; }
        public DateTime DateModification { get; set; }
        public int MenuId { get; set; }
        public ICollection<Menu> SousMenus { get;} = new List<Menu>();
        public int HabilitationProfilId { get; set; }
        public HabilitationProfil? HabilitationProfil { get; set; }
        public int ApplicationId { get; set; }
        public Application? Application { get; set; }
    }
}
