namespace LoanManagement.API.Validator.Users_Management
{
	public class UserUpdateValidator : AbstractValidator<UserUpdateRessource>
	{
        public UserUpdateValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .WithMessage("Le nom d'utilisateur doit être renseigné");

            RuleFor(x => x.OldPassword)
                .NotNull()
                .WithMessage("L'ancien mot de passe doit être renseigné");

            RuleFor(x => x.NewPassword)
                .NotNull()
                .WithMessage("Le nouveau mot de passe doit être renseigné");

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .WithMessage("Confirmez le nouveau mot de passe")
                .When(x => x.NewPassword == x.ConfirmPassword)
                .WithMessage("Les mots de passe sont différents");


             
        }
    }
}
