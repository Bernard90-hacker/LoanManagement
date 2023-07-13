namespace LoanManagement.API.Validator.Loan_Management
{
	public class CompteRessourceValidator : AbstractValidator<CompteRessource>
	{
        public CompteRessourceValidator()
        {
			RuleFor(x => x.NumeroCompte)
				.NotNull()
				.MaximumLength(13);

			RuleFor(x => x.ClientId)
				.NotNull();
        }
	}
}
