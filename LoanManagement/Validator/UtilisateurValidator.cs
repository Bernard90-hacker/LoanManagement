using FluentValidation;
using LoanManagement.API.Ressources;

namespace LoanManagement.API.Validator	
{
	public class UtilisateurValidator : AbstractValidator<UtilisateurRessource>
	{
        public UtilisateurValidator()
        {
			RuleFor(x => x.Username)
				.NotEmpty()
				.WithMessage("Le nom d'utilisateur doit être renseigné");

			RuleFor(x => x.IsAdmin)
				.NotEmpty()
				.WithMessage("Renseignez le champ Admin");

			RuleFor(x => x.IsSuperAdmin)
				.NotEmpty()
				.WithMessage("Renseignez le champ Super Admin");

			RuleFor(x => x.DateDesactivation)
				.NotNull()
				.When(x => x.Statut == 1)
				.WithMessage("La date de désactivation doit être renseignée si l'utilisateur est actif");

			RuleFor(x => x.DateDesactivation)
				.GreaterThan(x => x.DateExpirationCompte)
				.WithMessage("La date de désactivation du compte doit être supérieure à celle d'expiration");

		}


	}
}
