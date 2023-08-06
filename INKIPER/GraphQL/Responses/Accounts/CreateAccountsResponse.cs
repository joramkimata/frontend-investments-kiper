using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Accounts;

public class CreateAccountsResponse
{
    public MutationResponse<Account> createAccounts { get; set; }
}