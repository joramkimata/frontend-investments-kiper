using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Accounts;

public class GetAllAccountsPaginated
{
    public PaginatedDataResponse<AccountType> getAllAccountsPaginated { get; set; }
}