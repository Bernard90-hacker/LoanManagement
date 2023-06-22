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
        public string DateAjout { get; set; } = string.Empty;
        public string DateModification { get; set; } = string.Empty;
        public int ProfilId { get; set; }
        public Profil? Profil { get; set; }
        public List<Menu> Menus { get; set; } = new();
    }
}
