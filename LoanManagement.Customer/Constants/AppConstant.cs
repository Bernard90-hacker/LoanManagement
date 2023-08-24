namespace LoanManagement.Customer.Constants;

/// <summary>
/// 
/// </summary>
public class AppConstant
{    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpResponse"></param>
    /// <param name="sharedLocalizer"></param>
    /// <returns></returns>
    public static async Task<JQueryViewModel> GetResponseMessage(HttpResponseMessage httpResponse)
    {
        string typeMessage = TypeMessage.Warning.GetString(), 
            title = "Attention", 
            message = string.Empty, description = string.Empty;
        const int timeOut = 8000;
        IEnumerable<string> errors = null;

        var objectResult = await httpResponse.Content.ReadAsStringAsync();
        switch (httpResponse.StatusCode)
        {
            case HttpStatusCode.MethodNotAllowed:
                message = "Méthode non autorisée";
                description = "Source introuvable";

				break;
            case HttpStatusCode.BadRequest:
            {
                var apiBadRequest = JsonConvert.DeserializeObject<ApiBadRequestResponse>(objectResult);
                if (!apiBadRequest.IsNull() && !string.IsNullOrEmpty(apiBadRequest?.Message))
                {
                    message = apiBadRequest.Message;
                    description = apiBadRequest.Description;
                    errors = apiBadRequest.Errors;
                }

                var validationProblem = JsonConvert.DeserializeObject<ValidationProblem>(objectResult);
                if (!validationProblem.IsNull() && !string.IsNullOrEmpty(validationProblem?.Title))
                {
                    message = "Erreur produite";
                    description = validationProblem.Title;
                }
                break;
            }
            case HttpStatusCode.NotFound:
            {
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(objectResult);
                if (!apiResponse.IsNull() && !string.IsNullOrEmpty(apiResponse?.Message))
                {
                    message = "Erreur produite";
                    description = apiResponse.Description;
                }
                break;
            }
            case HttpStatusCode.Continue:
                break;
            case HttpStatusCode.SwitchingProtocols:
                break;
            case HttpStatusCode.Processing:
                break;
            case HttpStatusCode.EarlyHints:
                break;
            case HttpStatusCode.OK:
                break;
            case HttpStatusCode.Created:
                break;
            case HttpStatusCode.Accepted:
                break;
            case HttpStatusCode.NonAuthoritativeInformation:
                break;
            case HttpStatusCode.NoContent:
                break;
            case HttpStatusCode.ResetContent:
                break;
            case HttpStatusCode.PartialContent:
                break;
            case HttpStatusCode.MultiStatus:
                break;
            case HttpStatusCode.AlreadyReported:
                break;
            case HttpStatusCode.IMUsed:
                break;
            case HttpStatusCode.Ambiguous:
                break;
            case HttpStatusCode.Moved:
                break;
            case HttpStatusCode.Found:
                break;
            case HttpStatusCode.RedirectMethod:
                break;
            case HttpStatusCode.NotModified:
                break;
            case HttpStatusCode.UseProxy:
                break;
            case HttpStatusCode.Unused:
                break;
            case HttpStatusCode.RedirectKeepVerb:
                break;
            case HttpStatusCode.PermanentRedirect:
                break;
            case HttpStatusCode.Unauthorized:
                break;
            case HttpStatusCode.PaymentRequired:
                break;
            case HttpStatusCode.Forbidden:
                break;
            case HttpStatusCode.NotAcceptable:
                break;
            case HttpStatusCode.ProxyAuthenticationRequired:
                break;
            case HttpStatusCode.RequestTimeout:
                break;
            case HttpStatusCode.Conflict:
                break;
            case HttpStatusCode.Gone:
                break;
            case HttpStatusCode.LengthRequired:
                break;
            case HttpStatusCode.PreconditionFailed:
                break;
            case HttpStatusCode.RequestEntityTooLarge:
                break;
            case HttpStatusCode.RequestUriTooLong:
                break;
            case HttpStatusCode.UnsupportedMediaType:
                break;
            case HttpStatusCode.RequestedRangeNotSatisfiable:
                break;
            case HttpStatusCode.ExpectationFailed:
                break;
            case HttpStatusCode.MisdirectedRequest:
                break;
            case HttpStatusCode.UnprocessableEntity:
                break;
            case HttpStatusCode.Locked:
                break;
            case HttpStatusCode.FailedDependency:
                break;
            case HttpStatusCode.UpgradeRequired:
                break;
            case HttpStatusCode.PreconditionRequired:
                break;
            case HttpStatusCode.TooManyRequests:
                break;
            case HttpStatusCode.RequestHeaderFieldsTooLarge:
                break;
            case HttpStatusCode.UnavailableForLegalReasons:
                break;
            case HttpStatusCode.InternalServerError:
                break;
            case HttpStatusCode.NotImplemented:
                break;
            case HttpStatusCode.BadGateway:
                break;
            case HttpStatusCode.ServiceUnavailable:
                break;
            case HttpStatusCode.GatewayTimeout:
                break;
            case HttpStatusCode.HttpVersionNotSupported:
                break;
            case HttpStatusCode.VariantAlsoNegotiates:
                break;
            case HttpStatusCode.InsufficientStorage:
                break;
            case HttpStatusCode.LoopDetected:
                break;
            case HttpStatusCode.NotExtended:
                break;
            case HttpStatusCode.NetworkAuthenticationRequired:
                break;
            default:
                message = "Erreur non identifiée";
                break;
        }

        var jqueryViewModel = new JQueryViewModel
        {
            Title = title,
            TypeMessage = typeMessage,
            Message = message,
            Description = description,
            TimeOut = timeOut,
            Errors = errors
        };

        return jqueryViewModel;
    }
}
