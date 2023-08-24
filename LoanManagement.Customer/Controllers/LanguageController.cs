namespace LoanManagement.Customer.Controllers;

/// <summary>
/// 
/// </summary>
[Route("languages")]
public class LanguageController : Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="culture"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("change")]
    public IActionResult Change(string culture)
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(1) });

        var currentRoute = ConfigConstant.GetCurrentRouteFromSession(HttpContext);
        if (currentRoute == null) return RedirectToAction("Index", "Home");

        var currentAction = currentRoute.Split(",")[2];
        var currentController = currentRoute.Split(",")[1];
        var currentArea = currentRoute.Split(",")[0];

        return string.IsNullOrEmpty(currentArea)
            ? RedirectToAction(currentAction, currentController)
            : RedirectToAction(currentAction, currentController, new { area = currentArea });
    }
}
