using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses;

public class GetRolesResponse
{
    public List<RoleType> GetRoles { get; set; }
}