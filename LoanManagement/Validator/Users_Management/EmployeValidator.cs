namespace LoanManagement.API.Validator.Users_Management
{
	public class EmployeValidator : AbstractValidator<EmployeRessource>
	{
		public EmployeValidator()
		{
			RuleFor(x => x.Nom)
				.NotNull()
				.WithMessage("Le nom doit être renseigné");

			RuleFor(x => x.Prenoms)
				.NotNull()
				.WithMessage("Le prénom doit être renseigné");

			RuleFor(x => x.Email)
				.NotNull()
				.WithMessage("L'adresse mail doit être renseignée");

			RuleFor(x => x.Username)
				.NotNull()
				.WithMessage("L'utilisateur doit être renseigné");

			RuleFor(x => x.DepartementId)
				.NotNull()
				.WithMessage("Le département doit être renseigné");

			RuleFor(x => x.Matricule)
				.NotNull()
				.WithMessage("Le matricule doit être renseigné");

		}
	}
}
