namespace LoanManagement.API.Ressources.Users_Management
{
	public class HabilitationProfilRessource
	{
		public bool Edition { get; set; }
		public bool Insertion { get; set; }
		public bool Modification { get; set; }
		public bool Suppression { get; set; }
		public bool Generation { get; set; }
        public int ProfilId { get; set; }
    }
}
