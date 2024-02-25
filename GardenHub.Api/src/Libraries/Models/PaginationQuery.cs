namespace Models;

public class PaginationQuery
{
    public PaginationQuery()
    {
        PageNumber = 1;
        PageSize = 100;
    }

    public PaginationQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber <= 0 ? 1 : pageNumber;
        PageSize = pageSize > 100 || pageSize <= 0 ? 100 : pageSize;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
