using FluentAssertions;

namespace LoanManagement.API.Validator.Loan_Management
{
	public class InfoSanteClientRessourceValidator : AbstractValidator<InfoSanteClientRessource>
	{
        public InfoSanteClientRessourceValidator()
        {

			RuleFor(x => x.DossierClientId)
				.NotNull();

		}
	}
}
