namespace LoanManagement.API.Validator.Loan_Management
{
	public class RoleOrganeRessourceValidator : AbstractValidator<RoleOrganeRessource>	
	{
        public RoleOrganeRessourceValidator()
        {
            RuleFor(x => x.Libelle)
                .NotNull();

			RuleFor(x => x.DureeTraitement)
				.NotNull();
		}
    }
}
