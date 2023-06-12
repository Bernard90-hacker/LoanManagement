namespace CustomApiResponse.Models;

public class ValidationProblem
{
    public int StatusCode { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
