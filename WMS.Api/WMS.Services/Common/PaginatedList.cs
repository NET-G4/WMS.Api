namespace WMS.Services.Common;

public class PaginatedList<T>
{
    public List<T> Data { get; init; }
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }
    public int PagesCount { get; init; }
    public int TotalCount { get; init; }
    public bool HasNextPage { get; init; }
    public bool HasPreviousPage { get; init; }

    public PaginatedList()
    {
        Data = [];
    }

    public PaginatedList(List<T> data, int pageNumber, int pageSize, int totalCount)
    {
        Data = data;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        PagesCount = (int)Math.Ceiling((double)totalCount / pageSize);
        HasNextPage = pageNumber < PagesCount;
        HasPreviousPage = pageNumber > 1;
    }
}
