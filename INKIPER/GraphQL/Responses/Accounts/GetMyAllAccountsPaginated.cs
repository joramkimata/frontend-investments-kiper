using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Accounts;

public class GetMyAllAccountsPaginated
{
    public PaginatedDataResponse<AccountType> getMyAllAccountsPaginated { get; set; }
}