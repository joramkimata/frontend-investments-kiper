namespace INKIPER.GraphQL.Responses;

public class QueryResponse<T>
{
    public T Data { get; set; }
    public bool Error { get; set; }
    public string Message { get; set; }
}