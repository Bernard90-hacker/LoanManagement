namespace LoanManagement.API.Validator.Loan_Management
{
	public class PretAccordRessourceValidator : AbstractValidator<PretAccordRessource>
	{
        public PretAccordRessourceValidator()
        {
            RuleFor(x => x.SalaireNetMensuel)
                .NotNull();

			RuleFor(x => x.Mensualite)
				.NotNull();

			RuleFor(x => x.DateDepartRetraite)
				.NotNull();

			RuleFor(x => x.DossierClientId)
				.NotNull();

			RuleFor(x => x.MontantPrime) 
				.NotNull();

			RuleFor(x => x.Surprime)
				.NotNull();

			RuleFor(x => x.PeriodicitePaiementId)
				.NotNull();

			RuleFor(x => x.TypePretId)
				.NotNull();
		}
    }
}
