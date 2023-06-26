namespace LoanManagement.API.Validator.Users_Management
{
	public class ParamMotDePasseValidator : AbstractValidator<ParamMotDePasseRessource>
	{
        public ParamMotDePasseValidator()
        {
			RuleFor(x => x.IncludeSpecialCharacters)
				.NotNull()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.IncludeLowerCase)
				.NotNull()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.IncludeUpperCase)
				.NotNull()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.ExcludeUsername)
				.NotNull()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.Taille)
				.NotNull()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.DelaiExpiration)
				.NotNull()
				.WithMessage("Renseignez ce champ");

			RuleFor(x => x.IncludeDigits)
				.NotNull()
				.WithMessage("Renseignez ce champ");

		}
	}
}
