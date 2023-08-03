namespace LoanManagement.Client.ViewModels;

public class JQueryViewModel
{
	public string Title { get; set; }
	public string TypeMessage { get; set; }
	public string Message { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public int TimeOut { get; set; } = 5000;
	public IEnumerable<string> Errors { get; set; }
}
