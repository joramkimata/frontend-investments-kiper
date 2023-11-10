using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Users;

public class GetUserResponse
{
    public UserType getUser { get; set; }
    
    public List<RoleType> getRoles { get; set; }
 }