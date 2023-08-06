using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Accounts;

public class GetMyAllAccountsPaginated
{
    public PaginatedDataResponse<Account> getMyAllAccountsPaginated { get; set; }
}