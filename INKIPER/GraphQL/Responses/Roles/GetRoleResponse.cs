using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Roles;

public class GetRoleResponse
{
    public RoleType getRole { get; set; }
        
    public List<GroupPermissions> getAllPermissionsGroupedByPermissionGroupName { get; set; }
}