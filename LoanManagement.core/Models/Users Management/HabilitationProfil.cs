namespace LoanManagement.core.Models.Users_Management
{
	public class HabilitationProfil
	{
        public int Id { get; set; }
        public bool Edition { get; set; }
        public bool Insertion { get; set; }
        public bool Modification { get; set; }
        public bool Suppression { get; set; }
        public bool Generation { get; set; }
        public DateTime DateAjout { get; set; }
        public DateTime DateModification { get; set; }
        public int ProfilId { get; set; }
        public Profil? Profil { get; set; }
        public List<Menu> Menus { get; set; } = new();
    }
}
