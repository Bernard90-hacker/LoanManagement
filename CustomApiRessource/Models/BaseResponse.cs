namespace CustomApiResponse.Models;

public class BaseResponse
{
    public string Response { get; set; }   
    public string SuccessMessage { get; set; }   
    public string ErrorMessage { get; set; }   
    public string WarningMessage { get; set; }   
    public string InfoMessage { get; set; }   
    public string Statut { get; set; }   
    public string Message { get; set; }   
}
