namespace LoanManagement.API.Validator.Loan_Management
{
	public class EmployeurRessourceValidator : AbstractValidator<EmployeurRessource>
	{
        public EmployeurRessourceValidator()
        {
            RuleFor(x => x.Nom)
                .NotNull();

			RuleFor(x => x.Tel)
				.NotNull();


			RuleFor(x => x.BoitePostale)
					.NotNull();
		}
    }
}
