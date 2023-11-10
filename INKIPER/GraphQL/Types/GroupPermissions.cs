namespace INKIPER.GraphQL.Types;

public class GroupPermissions
{
    public string permissionGroupName { get; set; }
    public List<PermissionType> permissions { get; set; }
}