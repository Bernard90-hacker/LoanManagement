namespace LoanManagement.API.Validator.Loan_Management
{
	public class MembreOrganeRessourceValidator : AbstractValidator<MembreOrganeRessource>
	{
        public MembreOrganeRessourceValidator()
        {
            RuleFor(x => x.UtilisateurId)
                .NotNull();

			RuleFor(x => x.OrganeDecisionId)
				.NotNull();
		}
    }
}
