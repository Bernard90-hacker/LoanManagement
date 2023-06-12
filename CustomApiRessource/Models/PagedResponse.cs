namespace CustomApiResponse.Models;

public class PagedResponse<T>
{
    public IEnumerable<T> Data { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }

    public PagedResponse() { }

    public PagedResponse(IEnumerable<T> response)
    {
        Data = response;
    }

    public PagedResponse(IEnumerable<T> response, int totalCount, int pageSize, int currentPage, int totalPages, bool hasNext, bool hasPrevious)
    {
        Data = response;
        TotalCount = totalCount;
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalPages = totalPages;
        HasNext = hasNext;
        HasPrevious = hasPrevious;
    }
}
