namespace LoanManagement.API.Validator.Users_Management
{
	public class UpdateApplicationStatusValidator : AbstractValidator<UpdateApplicationStatusRessource>
	{
        public UpdateApplicationStatusValidator()
        {
            RuleFor(x => x.Status)
                .NotNull()
                .WithMessage("Le statut de l'application doit être renseigné");

            RuleFor(x => x.ApplicationCode)
                .NotNull()
                .WithMessage("Le code de l'application doit être renseigné");
        }
    }
}
