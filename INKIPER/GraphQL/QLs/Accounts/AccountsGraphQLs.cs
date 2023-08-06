namespace INKIPER.GraphQL.QLs.Accounts;

public class AccountsGraphQLs
{
    public static String GET_ALL_ACCOUNTS_PAGINATED = @"
      query getAllAccountsPaginated($input: PaginatedInput!) {
        getAllAccountsPaginated(input: $input) {
          totalPages
          items {
            uuid
            name
            accountNumber
            description
          }
          totalCount
        }
      }
    ";
    
    public static String GET_MY_ALL_ACCOUNTS_PAGINATED = @"
      query getMyAllAccountsPaginated($input: PaginatedInput!) {
        getMyAllAccountsPaginated(input: $input) {
          totalPages
          items {
            uuid
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
    
    public static String UPDATE_ACCOUNTS = @"
      mutation updateAccounts($uuid: String!, $input: AccountsInput!) {
        updateAccounts(uuid: $uuid, input: $input) {
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