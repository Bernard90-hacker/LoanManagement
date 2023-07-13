namespace LoanManagement.API.Validator.Loan_Management
{
	public class UpdateDeroulementRessourceValidator : AbstractValidator<UpdateDeroulementRessource>
	{
        public UpdateDeroulementRessourceValidator()
        {
			RuleFor(x => x.Id)
				.NotNull();

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
