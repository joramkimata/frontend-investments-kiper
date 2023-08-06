using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Accounts;

public class UpdateAccountsResponse
{
    public MutationResponse<Account> updateAccounts { get; set; }
}