namespace LoanManagement.API.Validator.Users_Management
{
	public class MenuRessourceValidator : AbstractValidator<MenuRessource>
	{
        public MenuRessourceValidator()
        {

			RuleFor(x => x.Libelle)
				.NotNull()
				.WithMessage("Le libellé doit être renseigné");

			RuleFor(x => x.Description)
				.NotNull()
				.WithMessage("La description doit être renseignée");

			RuleFor(x => x.Statut)
				.NotNull()
				.WithMessage("Le statut doit être renseigné");

			RuleFor(x => x.Position)
				.NotNull()
				.WithMessage("La position doit être renseignée");

			RuleFor(x => x.ApplicationId)
				.NotNull()
				.WithMessage("Le module doit être renseigné");

			RuleFor(x => x.HabilitationProfilId)
				.NotNull()
				.WithMessage("Le droit doit être renseigné");
		}
	}
}
