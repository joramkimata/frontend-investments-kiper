using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Roles;

public class CreateRoleResponse
{
    public MutationResponse<RoleType> createRole { get; set; }
}