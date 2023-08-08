namespace INKIPER.GraphQL.QLs.Deposit;

public class DepositGraphQLs
{
    public static String GET_MY_ALL_DEPOSITS_PAGENATED = @"
      query getMyAllDepositsPaginated($input: PaginatedInput!) {
        getMyAllDepositsPaginated(input: $input) {
          totalPages
          items {
            uuid
            accounts {
              uuid
              name
              accountNumber
            }
            amountDeposited
            description
            depositedDate
          }
          totalCount
        }
      }
    ";

    public static string CREATE_DEPOSIT = @"
        mutation createDeposits($input: DepositInput!) {
          createDeposits(input: $input) {
            code
            data {
              uuid
            }
            status
            errorDescription
          }
        }
    ";

    public static string UPDATE_DEPOSIT = @"
      mutation updateDeposits($uuid: String!, $input: DepositInput!) {
        updateDeposits(uuid: $uuid, input: $input) {
          code
          data {
            uuid
          }
          status
          errorDescription
        }
      }
    ";

    public static string DELETE_DEPOSIT = @"
        mutation deleteDeposits($uuid: String!) {
          deleteDeposits(uuid: $uuid) {
            code
            data {
              uuid
            }
            status
            errorDescription
          }
        }
    ";

    public static string GET_ALL_DEPOSITS = @"
      query getAllDeposits {
        getAllDeposits {
          uuid
          accounts {
            uuid
            name
            accountNumber
          }
          amountDeposited
          description
          depositedDate
        }
      }
    ";
}