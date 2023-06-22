namespace LoanManagement.API.Validator.Users_Management
{
	public class HabilitationProfilValidator : AbstractValidator<HabilitationProfilRessource>
	{
        public HabilitationProfilValidator()
        {
			RuleFor(x => x.Edition)
				.NotEmpty()
				.WithMessage("Le droit d'édition doit être renseigné");

			RuleFor(x => x.Suppression)
				.NotEmpty()
				.WithMessage("Le droit de suppression doit être renseigné");

			RuleFor(x => x.Generation)
				.NotEmpty()
				.WithMessage("Le droit de génération doit être renseigné");

			RuleFor(x => x.Insertion)
				.NotEmpty()
				.WithMessage("Le droit d'insertion doit être renseigné");

			RuleFor(x => x.Modification)
				.NotEmpty()
				.WithMessage("Le droit de suppression doit être renseigné");
		}
	}
}
