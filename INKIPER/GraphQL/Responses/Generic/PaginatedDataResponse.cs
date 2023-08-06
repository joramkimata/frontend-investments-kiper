namespace INKIPER.GraphQL.Responses;

public class PaginatedDataResponse<T>
{
    public int totalPages { get; set; }
    public int totalCount { get; set; }
    public T[] items { get; set; }
}