using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Accounts;

public class UpdateAccountsResponse
{
    public MutationResponse<AccountType> updateAccounts { get; set; }
}