using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Accounts;

public class SearchAccountsPaginatedResponse
{
    public PaginatedDataResponse<AccountType> searchAccountsPaginated { get; set; }
}