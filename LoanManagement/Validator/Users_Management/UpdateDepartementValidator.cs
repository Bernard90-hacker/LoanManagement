namespace LoanManagement.API.Validator.Users_Management
{
	public class UpdateDepartementValidator : AbstractValidator<UpdateDepartementRessource>
	{
        public UpdateDepartementValidator()
        {
			RuleFor(x => x.Libelle)
				.NotEmpty()
				.WithMessage("Le libellé doit être renseigné")
				.MaximumLength(100)
				.WithMessage("Le libellé ne doit pas comprendre plus de 100 caractères");

			RuleFor(x => x.Code)
				.NotEmpty()
				.WithMessage("Le code du département doit être renseigné");
		}
    }
}
