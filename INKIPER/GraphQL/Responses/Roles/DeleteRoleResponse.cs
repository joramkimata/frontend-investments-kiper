using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Roles;

public class DeleteRoleResponse
{
    public MutationResponse<RoleType> deleteRole { get; set; }
}