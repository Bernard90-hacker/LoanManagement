namespace LoanManagement.API.Validator.Users_Management
{
	public class ApplicationValidator : AbstractValidator<ApplicationRessource>
	{
		public ApplicationValidator()
		{

			RuleFor(x => x.Libelle)
				.NotNull()
				.MaximumLength(30)
				.WithMessage("Le libellé doit être renseigné et ne" +
				" doit pas dépasser plus de 30 caractères");

			RuleFor(x => x.Description)
				.NotNull()
				.WithMessage("La description doit être renseignée");

			RuleFor(x => x.Version)
				.NotNull()
				.WithMessage("La version doit être renseignée");

			RuleFor(x => x.Statut)
				.NotNull()
				.WithMessage("La version doit être renseignée");
		}
	}



}
