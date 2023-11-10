namespace INKIPER.GraphQL.Types;

public class PermissionType: BaseType
{
    public string name { get; set; }
    public string description { get; set; }
    public string permissionGroupName { get; set; }
    public bool belongToThisRole { get; set; }
}