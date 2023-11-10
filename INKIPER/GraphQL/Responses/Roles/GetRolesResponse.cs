using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Roles;

public class GetRolesResponse
{
    public List<RoleType> GetRoles { get; set; }
}