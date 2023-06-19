namespace LoanManagement.API.Validator.Users_Management
{
	public class SaveDirectionValidator : AbstractValidator<DirectionRessource>
	{
        public SaveDirectionValidator()
        {
			RuleFor(x => x.Code)
			  .NotEmpty()
			  .WithMessage("Le code doit être renseigné")
			  .MaximumLength(12)
			  .WithMessage("Le code ne doit pas comprendre plus de 12 chiffres");

			RuleFor(x => x.Libelle)
				.NotEmpty()
				.WithMessage("Le libellé doit être renseigné")
				.MaximumLength(100)
				.WithMessage("Le libellé ne doit pas comprendre plus de 100 caractères");
		}
    }
}
