namespace LoanManagement.API.Validator.Loan_Management
{
	public class ClientRessourceValidator : AbstractValidator<ClientRessource>
	{
        public ClientRessourceValidator()
        {
			RuleFor(x => x.Indice)
				.NotNull()
				.WithMessage("L'indice ne doit pas être nul");

			RuleFor(x => x.Nom)
				.NotNull()
				.WithMessage("Le nom doit être renseigné");

			RuleFor(x => x.Prenoms)
				.NotNull()
				.WithMessage("Le(s) prénom(s) doit(vent) être renseigné(s)");

			RuleFor(x => x.DateNaissance)
				.NotNull()
				.WithMessage("La date de naissance doit être renseignée");

			RuleFor(x => x.Residence)
				.NotNull()
				.WithMessage("La résidence doit être renseignée");

			RuleFor(x => x.Ville)
				.NotNull()
				.WithMessage("La ville doit être renseignée");

			RuleFor(x => x.Quartier)
				.NotNull()
				.WithMessage("Le quartier doit être renseigné");

			RuleFor(x => x.Profession)
				.NotNull()
				.WithMessage("La profession doit être renseignée");
		}
	}
}
