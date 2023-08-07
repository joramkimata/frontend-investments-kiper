using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Accounts;

public class DeleteAccountsResponse
{
    public MutationResponse<Account> deleteAccounts { get; set; }
}