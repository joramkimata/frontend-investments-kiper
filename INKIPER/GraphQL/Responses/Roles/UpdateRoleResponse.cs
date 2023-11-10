using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Roles;

public class UpdateRoleResponse
{
    public MutationResponse<RoleType> updateRole { get; set; }
}