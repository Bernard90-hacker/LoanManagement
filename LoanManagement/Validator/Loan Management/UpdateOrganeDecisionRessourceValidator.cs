namespace LoanManagement.API.Validator.Loan_Management
{
	public class UpdateOrganeDecisionRessourceValidator : AbstractValidator<UpdateOrganeDecisionRessource>
	{
        public UpdateOrganeDecisionRessourceValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();

			RuleFor(x => x.Libelle)
				.NotNull();

			RuleFor(x => x.RoleOrganeId)
				.NotNull();
		}
    }
}
