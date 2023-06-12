using CustomApiRessource.Enums;

namespace CustomApiResponse.Models;

public class ApiOkWithWarningResponse : ApiResponse
{
    public IEnumerable<string> Warnings { get; }
    public new string Description { get; }

    public ApiOkWithWarningResponse(IEnumerable<string> warnings) : base((int)CustomHttpCode.WARNING)
    {
        Warnings = warnings;
    }

    public ApiOkWithWarningResponse(IEnumerable<string> warnings, string description) : base((int)CustomHttpCode.WARNING, description: description)
    {
        Warnings = warnings;
        Description = description;
    }
}
