using Microsoft.AspNetCore.Http;

namespace LoanManagement.core.Models.Users_Management
{
	public class Profil
	{
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Libelle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateExpiration { get; set; }
        public string Statut { get; set; } = string.Empty;
        public DateTime DateAjout { get; set; }
        public DateTime DateModification { get; set; }
        public List<HabilitationProfil> Habilitations { get; set; } = new();
    }
}
