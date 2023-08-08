using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Accounts;

public class CreateAccountsResponse
{
    public MutationResponse<AccountType> createAccounts { get; set; }
}