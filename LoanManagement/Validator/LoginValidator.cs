using FluentValidation;

namespace LoanManagement.API.Validator
{
	public class LoginValidator : AbstractValidator<LoginRessource>
	{
        public LoginValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty()
                .WithMessage("Email field can not be null")
                .Must(e => e.Contains("@gmail.com"))
                .WithMessage("Incorrect email format");

            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage("Password fied can not be empty")
                .Must(p => p.Length > 4)
                .WithMessage("Password must contain more than 4 characters");
        }
    }
}
