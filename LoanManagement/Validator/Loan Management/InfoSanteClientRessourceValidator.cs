using FluentAssertions;

namespace LoanManagement.API.Validator.Loan_Management
{
	public class InfoSanteClientRessourceValidator : AbstractValidator<InfoSanteClientRessource>
	{
        public InfoSanteClientRessourceValidator()
        {
			RuleFor(x => x.ReponseBoolenne)
				.NotNull();

			RuleFor(x => x.NatureQuestionId)
				.NotNull();

			RuleFor(x => x.DossierClientId)
				.NotNull();

			RuleFor(x => x.ReponsePrecision)
				.NotNull()
				.When(x => x.ReponseBoolenne == false);

			RuleFor(x => x.DureeTraitement)
				.NotNull()
				.When(x => x.ReponseBoolenne == false);

			RuleFor(x => x.PeriodeTraitement)
				.NotNull()
				.When(x => x.ReponseBoolenne == false);

			RuleFor(x => x.LieuTraitement)
				.NotNull()
				.When(x => x.ReponseBoolenne == false);

		}
	}
}
