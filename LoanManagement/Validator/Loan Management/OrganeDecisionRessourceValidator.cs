namespace LoanManagement.API.Validator.Loan_Management
{
	public class OrganeDecisionRessourceValidator : AbstractValidator<OrganeDecisionRessource>
	{
        public OrganeDecisionRessourceValidator()
        {
            RuleFor(x => x.Libelle)
                .NotNull();
        }
    }
}
