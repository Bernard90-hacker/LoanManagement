using Microsoft.AspNetCore.Http;

namespace LoanManagement.core.Models.Users_Management
{
	public class Profil
	{
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Libelle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DateExpiration { get; set; } = string.Empty;
        public int Statut { get; set; }
        public string DateAjout { get; set; } = string.Empty;
        public string DateModification { get; set; } = string.Empty;
        public int UtilisateurId { get; set; }
        public virtual Utilisateur? Utilisateur { get; set; }
        public virtual HabilitationProfil? Habilitation { get; set; }
    }
}
