namespace LoanManagement.API.Validator.Users_Management
{
    public class UpdateParamMotDePasseValidator : AbstractValidator<UpdateParamMotDePasseRessource>
    {
        public UpdateParamMotDePasseValidator()
        {
            RuleFor(x => x.DelaiExpiration)
                .NotNull();

            RuleFor(x => x.Id)
                .NotNull();

            RuleFor(x => x.IncludeSpecialCharacters)
                .NotNull();

            RuleFor(x => x.IncludeLowerCase)
                .NotNull();

            RuleFor(x => x.IncludeUpperCase)
                .NotNull();

            RuleFor(x => x.Taille)
                .NotNull();

            RuleFor(x => x.IncludeDigits)
                .NotNull();

            RuleFor(x => x.ExcludeUsername)
                .NotNull();
        }
    }
}
