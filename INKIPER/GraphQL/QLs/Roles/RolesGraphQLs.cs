namespace INKIPER.GraphQL;

public class RolesGraphQLs
{
    public static String GET_ROLES = @"
        query getRoles {
          getRoles {
            uuid
          }
        }
    ";
}