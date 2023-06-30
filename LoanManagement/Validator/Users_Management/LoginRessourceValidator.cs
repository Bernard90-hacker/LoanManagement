namespace LoanManagement.API.Validator.Users_Management
{
	public class LoginRessourceValidator : AbstractValidator<LoginRessource>
	{
        public LoginRessourceValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .WithMessage("Le nom d'utilisateur doit être renseigné");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Le mot de passe doit être renseigné")
                .MaximumLength(30);
        }
    }
}
