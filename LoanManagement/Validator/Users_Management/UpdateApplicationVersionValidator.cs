namespace LoanManagement.API.Validator.Users_Management
{
	public class UpdateApplicationVersionValidator : AbstractValidator<UpdateApplicationVersionRessource>
	{
        public UpdateApplicationVersionValidator()
        {
			RuleFor(x => x.ApplicationCode)
				.NotNull()
				.WithMessage("Le code de l'application doit être renseigné");

			RuleFor(x => x.Version)
				.NotNull()
				.WithMessage("La version de l'application doit être renseignée");


		}
	}
}
