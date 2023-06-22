namespace LoanManagement.API.Validator.Users_Management
{
	public class ProfilValidator : AbstractValidator<ProfilRessource>
	{
        public ProfilValidator()
        {
			RuleFor(x => x.Code)
				.NotEmpty()
				.WithMessage("Le code doit être renseigné");

			RuleFor(x => x.Libelle)
				.NotEmpty()
				.WithMessage("Le libellé doit être renseigné");

			RuleFor(x => x.Description)
				.NotEmpty()
				.WithMessage("La description doit être renseignée");

			RuleFor(x => x.DateExpiration)
				.NotEmpty()
				.WithMessage("La date d'expiration doit être renseignée");

			RuleFor(x => x.Statut)
				.NotEmpty()
				.WithMessage("Le statut doit être renseigné");
		}
	}
}
