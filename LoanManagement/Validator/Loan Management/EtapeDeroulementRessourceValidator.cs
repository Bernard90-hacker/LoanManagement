namespace LoanManagement.API.Validator.Loan_Management
{
	public class EtapeDeroulementRessourceValidator : AbstractValidator<EtapeDeroulementRessource>
	{
        public EtapeDeroulementRessourceValidator()
        {
            RuleFor(x => x.Etape)
                .NotNull();

			RuleFor(x => x.MembreOrganeId)
				.NotNull();

			RuleFor(x => x.DeroulementId)
				.NotNull();
		}
    }
}
