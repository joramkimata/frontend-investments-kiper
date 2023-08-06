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
}