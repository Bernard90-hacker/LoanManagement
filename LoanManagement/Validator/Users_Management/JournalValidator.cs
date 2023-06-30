namespace LoanManagement.API.Validator.Users_Management
{
	public class JournalValidator : AbstractValidator<JournalRessource>
	{
		public JournalValidator()
		{
			RuleFor(x => x.Niveau)
				.NotNull()
				.WithMessage("Le niveau doit être renseigné");

			RuleFor(x => x.Libelle)
				.NotNull()
				.WithMessage("Le libellé doit être renseigné");

			RuleFor(x => x.TypeJournalCode)
				.NotNull()
				.WithMessage("Le code du type de journal doit être renseigné");

			RuleFor(x => x.Username)
				.NotNull()
				.WithMessage("Le nom d'utilisateur doit être renseigné");

		}
	}
}
