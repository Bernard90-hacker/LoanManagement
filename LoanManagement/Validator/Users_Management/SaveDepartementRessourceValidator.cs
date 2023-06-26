namespace LoanManagement.API.Validator.Users_Management
{
	public class SaveDepartementRessourceValidator : AbstractValidator<SaveDepartementRessource>
	{
        public SaveDepartementRessourceValidator()
        {
			RuleFor(x => x.Libelle)
				.NotEmpty()
				.WithMessage("Le libellé doit être renseigné")
				.MaximumLength(100)
				.WithMessage("Le libellé ne doit pas comprendre plus de 100 caractères");

			RuleFor(x => x.DirectionCode)
				.NotEmpty()
				.WithMessage("Le code de la direction doit être renseigné");
		}
	}
}
