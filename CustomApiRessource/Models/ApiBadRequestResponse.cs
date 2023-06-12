
using CustomApiRessource.Enums;

namespace CustomApiResponse.Models
{
	public class ApiBadRequestResponse : ApiResponse
	{
		public IEnumerable<string> Errors { get; set; }

		public ApiBadRequestResponse() { }

		public ApiBadRequestResponse(IEnumerable<string> errors) : base((int)CustomHttpCode.ERROR)
		{
			Errors = errors;
		}

		public ApiBadRequestResponse(ModelStateDictionary modelState) : base((int)HttpCode.BAD_REQUEST)
		{
			if (modelState.IsValid)
			{
				throw new ArgumentException("L'état du modèle doit être invalide", nameof(modelState));
			}

			Errors = modelState.SelectMany(x => x.Value.Errors)
				.Select(x => x.ErrorMessage).ToArray();
		}

		public ApiBadRequestResponse(FluentValidation.Results.ValidationResult validationResult) : base((int)HttpCode.BAD_REQUEST)
		{
			if (validationResult.IsValid)
			{
				throw new ArgumentException("L'état du modèle doit être invalide", nameof(validationResult));
			}

			Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray();
		}
	}

}


