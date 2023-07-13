namespace LoanManagement.API.Validator.Loan_Management
{
	public class ClientLoginValidator : AbstractValidator<ClientLoginRessource>
	{
        public ClientLoginValidator()
        {
            RuleFor(x => x.Indice)
                .NotNull()
                .WithMessage("L'indice doit être renseigné");

            RuleFor(x => x.Telephone)
                .NotNull()
                .WithMessage("Le téléphone doit être renseigné");
        }
    }
}
