namespace LoanManagement.API.Validator.Users_Management
{
	public class SaveTypeJournalValidator : AbstractValidator<TypeJournalRessource>
	{
        public SaveTypeJournalValidator()
        {
			RuleFor(x => x.Code)
				.NotNull()
				.WithMessage("Le code doit être renseigné");

			RuleFor(x => x.Libelle)
				.NotNull()
				.WithMessage("Le Libellé doit être renseigné");

			RuleFor(x => x.Statut)
				.NotNull()
				.WithMessage("Le statut doit être renseigné");
		}
	}
}
