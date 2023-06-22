namespace LoanManagement.API.Validator.Users_Management
{
	public class ParamMotDePasseValidator : AbstractValidator<ParamMotDePasseRessource>
	{
        public ParamMotDePasseValidator()
        {
			RuleFor(x => x.IncludeSpecialCharacters)
				.NotEmpty()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.IncludeLowerCase)
				.NotEmpty()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.IncludeUpperCase)
				.NotEmpty()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.ExcludeUsername)
				.NotEmpty()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.Taille)
				.NotEmpty()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.DelaiExpiration)
				.NotEmpty()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.IncludeDigits)
				.NotEmpty()
				.WithMessage("Renseignez ce champ");

		}
	}
}
