using FluentValidation;

namespace LoanManagement.API.Validator.Loan_Management
{
	public class DeroulementRessourceValidator : AbstractValidator<DeroulementRessource>
	{
        public DeroulementRessourceValidator()
        {
			RuleFor(x => x.Plancher)
				.NotNull();

			RuleFor(x => x.Plafond)
				.NotNull()
				.GreaterThan(x => x.Plancher);

			RuleFor(x => x.Libelle)
				.NotNull();

			RuleFor(x => x.NiveauInstance)
				.NotNull();

			RuleFor(x => x.TypePretId)
				.NotNull();

		}

	}
}
