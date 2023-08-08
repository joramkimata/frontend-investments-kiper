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

    public static String DELETE_ACCOUNTS = @"
      mutation deleteAccounts($uuid: String!) {
        deleteAccounts(uuid: $uuid) {
          code
          data {
            uuid
          }
          status
          errorDescription
        }
      }
    ";

    public static String SEARCH_ACCOUNTS = @"
      query searchAccountsPaginated($searchTerm: String!, $input: PaginatedInput!) {
        searchAccountsPaginated(searchTerm: $searchTerm, input: $input) {
          totalPages
          totalCount
          items {
            uuid
            name
            deleted
            accountNumber
            description
          }
        }
      }    
    ";

    public static String GET_ALL_ACCOUNTS = @"
      query getAMyAllAccounts {
        getAMyAllAccounts {
          name
          uuid
          accountNumber
          deleted
          description
        }
      }
    ";
}