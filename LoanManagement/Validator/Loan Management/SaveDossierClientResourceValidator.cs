namespace LoanManagement.API.Validator.Loan_Management
{
    public class SaveDossierClientResourceValidator : AbstractValidator<SaveDossierClientResource>
    {
        public SaveDossierClientResourceValidator()
        {
            RuleFor(x => x.AttestationTravail)
                .NotNull();

            RuleFor(x => x.ContratTravail)
                .NotNull();

            RuleFor(x => x.Poids)
                .NotNull();

            RuleFor(x => x.Taille)
                .NotNull();

            RuleFor(x => x.TensionArterielle)
                .NotNull();

            RuleFor(x => x.PremierBulletinSalaire)
                .NotNull();

            RuleFor(x => x.DeuxiemeBulletinSalaire)
                .NotNull();

            RuleFor(x => x.TroisiemeBulletinSalaire)
                .NotNull();

            RuleFor(x => x.FactureProFormat)
                .NotNull();

            RuleFor(x => x.Fumeur)
                .NotNull();

            RuleFor(x => x.Buveur)
                .NotNull();

            RuleFor(x => x.CarteIdentite)
                .NotNull();

            RuleFor(x => x.EcheanceCarteIdentite)
                .NotNull();

            RuleFor(x => x.EstInfirme)
                .NotNull();

            RuleFor(x => x.Distractions)
                .NotNull();

            RuleFor(x => x.EstSportif)
                .NotNull();

            RuleFor(x => x.StatutMaritalId)
                .NotNull();

            RuleFor(x => x.ClientId)
                .NotNull();
        }
    }
}
