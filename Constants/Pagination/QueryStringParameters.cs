namespace Constants.Pagination
{
	public class QueryStringParameters
	{
		private const int MaxPageSize = 50;
		public int PageNumber { get; set; } = 1;
		//public string OrderBy { get; set; }   
		//public string Fields { get; set; }   

		private int _pageSize = 20;
		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
		}
	}
}
