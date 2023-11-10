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

    public static string GET_USERS_PAGINATED = @"
        query getAllUserPaginated($input: PaginatedInput!) {
          getAllUserPaginated(input: $input) {
            totalPages
            items {
              uuid 
              fullName
              phoneNumber
              email
              username
              userType
              active
              createdAt
            }
            totalCount
          }
        }
    ";

    public static string CHANGE_USER_PASSWORD = @"
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

    public static string ACTIVATE_USER = @"
      mutation activateUser($uuid: String!) {
        activateUser(uuid: $uuid) {
          code
          data {
            uuid
          }
          status
          errorDescription
        }
      }
    ";
    
    public static string BLOCK_USER = @"
      mutation blockUser($uuid: String!) {
        blockUser(uuid: $uuid) {
          code
          data {
            uuid
          }
          status
          errorDescription
        }
      }
    ";

    public static string GET_USER_INFO = @"
        query getUser($uuid: String!) {
          getUser(uid: $uuid) {
            uuid
            fullName
            email
            userType
            roles {
              uuid
              name
              displayName
              permissions {
                uuid
                name
              }
            }
          }
          getRoles {
              uuid
              name
              displayName
          }
        }
    ";

    public static string ASSIGN_ROLES = @"
      mutation assignRoles($assignRolesInput: AssignRolesInput!) {
        assignRoles(assignRolesInput: $assignRolesInput) {
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