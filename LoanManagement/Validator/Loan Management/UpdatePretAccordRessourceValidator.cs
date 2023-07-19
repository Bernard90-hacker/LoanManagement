namespace LoanManagement.API.Validator.Loan_Management
{
	public class UpdatePretAccordRessourceValidator : AbstractValidator<UpdatePretAccordRessource>
	{
        public UpdatePretAccordRessourceValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();

			RuleFor(x => x.MontantPret)
				.NotNull();

			RuleFor(x => x.DatePremiereEcheance)
				.NotNull();

			RuleFor(x => x.DateDepartRetraite)
				.NotNull();

			RuleFor(x => x.DateDerniereEcheance)
				.NotNull();

			RuleFor(x => x.MontantPrime)
				.NotNull();

			RuleFor(x => x.Surprime)
				.NotNull();

			RuleFor(x => x.SalaireNetMensuel)
				.NotNull();

			RuleFor(x => x.Mensualite)
				.NotNull();

			RuleFor(x => x.TypePretId)
				.NotNull();

			RuleFor(x => x.PeriodicitePaiementId)
				.NotNull();

			RuleFor(x => x.DossierClientId)
				.NotNull();
		}
	}
}
