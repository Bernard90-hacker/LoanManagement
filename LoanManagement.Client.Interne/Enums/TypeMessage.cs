namespace LoanManagement.Client.Interne.Enums;

public enum TypeMessage
{
	Success,
	Error,
	Warning
}

public static class TypeMessagesExtensions
{
	public static string GetString(this TypeMessage typeMessage)
	{
		return typeMessage switch
		{
			TypeMessage.Success => "success",
			TypeMessage.Error => "error",
			TypeMessage.Warning => "warning",
			_ => "info",
		};
	}
}
