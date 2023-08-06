namespace INKIPER.GraphQL.Inputs;

public class PaginatedInput
{
    public required int pageNumber { get; set; }
    public required int pageSize { get; set; }
}