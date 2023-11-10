using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Roles;

public class AssignPermissionsResponse
{
    public MutationResponse<RoleType> assignPermissions { get; set; }
}