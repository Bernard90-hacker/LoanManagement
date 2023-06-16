using FluentValidation;

namespace LoanManagement.API.Validator
{
    public class RegisterValidator : AbstractValidator<RegisterRessource>
    {
        public RegisterValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email field can not be null")
                .Must(e => e.EndsWith("@gmail.com"))
                .WithMessage("Incorrect email format");

            RuleFor(u => u.FirstName)
                .NotEmpty()
                .WithMessage("First Name field can not be null")
                .MaximumLength(30);

            RuleFor(u => u.LastName)
               .NotEmpty()
               .WithMessage("Last Name field can not be null")
               .MaximumLength(30);

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Password field can not be null")
                .When(u => u.Password.Length >= 4)
                .WithMessage("Password can not be less than 4 four characters");

            RuleFor(u => u.PasswordConfirm)
                .NotEmpty()
                .WithMessage("Password Confirm field can not be null")
                .When(u => u.Password == u.PasswordConfirm)
                .WithMessage("Password do not match");
        }
    }
}
