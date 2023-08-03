namespace INKIPER.GraphQL;

public class GraphQLs
{
    public static String GET_ROLES = @"
        query getRoles {
          getRoles {
            uuid
          }
        }
    ";
}