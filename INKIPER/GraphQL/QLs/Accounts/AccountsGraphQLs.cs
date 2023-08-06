namespace INKIPER.GraphQL.QLs.Accounts;

public class AccountsGraphQLs
{
    public static String GET_ALL_ACCOUNTS_PAGINATED = @"
      query getAllAccountsPaginated($input: PaginatedInput!) {
        getAllAccountsPaginated(input: $input) {
          totalPages
          items {
            name
            accountNumber
            description
          }
          totalCount
        }
      }
    ";

    public static String CREATE_ACCOUNTS = @"
      mutation createAccounts($input: AccountsInput!) {
        createAccounts(input: $input) {
          code
          data {
            uuid
          }
          status
          errorDescription
        }
      }
    ";
}