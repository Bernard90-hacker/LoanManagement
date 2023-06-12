using CustomApiRessource.Enums;

namespace CustomApiResponse.Models;

public class ApiOkResponse : ApiResponse
{
    public object Results { get; }

    public ApiOkResponse() { }

    public ApiOkResponse(object result) : base((int)HttpCode.OK)
    {
        Results = result;
    }
}
