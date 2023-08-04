namespace INKIPER.GraphQL.QLs.Users;

public class UsersGraphQLs
{
    public static String CHANGE_PASSWORD = @"
          mutation changeUserPassword(
            $uuid: String!
            $password: String!
            $confirmPassword: String!
          ) {
            changeUserPassword(
              uuid: $uuid
              password: $password
              confirmPassword: $confirmPassword
            ) {
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