using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Accounts;

public class GetAMyAllAccountsResponse
{
    public List<AccountType> getAMyAllAccounts { get; set; }
}