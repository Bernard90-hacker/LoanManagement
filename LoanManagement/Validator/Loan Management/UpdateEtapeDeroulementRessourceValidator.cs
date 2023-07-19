namespace LoanManagement.API.Validator.Loan_Management
{
	public class UpdateEtapeDeroulementRessourceValidator : AbstractValidator<UpdateEtapeDeroulementRessource>
	{
        public UpdateEtapeDeroulementRessourceValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();

			RuleFor(x => x.Etape)
				.NotNull();

			RuleFor(x => x.MembreOrganeId)
				.NotNull();

			RuleFor(x => x.DeroulementId)
				.NotNull();
		}
    }
}
