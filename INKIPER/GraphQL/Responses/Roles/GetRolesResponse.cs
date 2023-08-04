using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses;

public class GetRolesResponse
{
    public List<Role> GetRoles { get; set; }
}