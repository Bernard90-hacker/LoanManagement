using FluentValidation;
using LoanManagement.API.Ressources;

namespace LoanManagement.API.Validator
{
	public class UserValidator : AbstractValidator<UserRessource>
	{
		public UserValidator()
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
		}
	}
}
