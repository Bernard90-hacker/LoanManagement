namespace LoanManagement.API.Validator.Users_Management
{
	public class HabilitationProfilValidator : AbstractValidator<HabilitationProfilRessource>
	{
        public HabilitationProfilValidator()
        {
			RuleFor(x => x.Edition)
				.NotNull()
				.WithMessage("Le droit d'édition doit être renseigné");

			RuleFor(x => x.Suppression)
				.NotNull()
				.WithMessage("Le droit de suppression doit être renseigné");

			RuleFor(x => x.Generation)
				.NotNull()
				.WithMessage("Le droit de génération doit être renseigné");

			RuleFor(x => x.Insertion)
				.NotNull()
				.WithMessage("Le droit d'insertion doit être renseigné");

			RuleFor(x => x.Modification)
				.NotNull()
				.WithMessage("Le droit de suppression doit être renseigné");
		}
	}
}
