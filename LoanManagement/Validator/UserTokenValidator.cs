using FluentValidation;
using LoanManagement.API.Ressources;

namespace LoanManagement.API.Validator
{
	public class UserTokenValidator : AbstractValidator<UserTokenRessource>
	{
		public UserTokenValidator()
		{
			RuleFor(u => u.UserId).NotEmpty();
			RuleFor(u => u.CreatedAt).NotEmpty();
			RuleFor(u => u.ExpiredAt).NotEmpty();
			RuleFor(u => u.Token).NotEmpty();
		}
	}
}
