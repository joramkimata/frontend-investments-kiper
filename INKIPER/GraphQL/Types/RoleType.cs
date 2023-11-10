namespace INKIPER.GraphQL.Types;

public class RoleType: BaseType
{
    public string name { get; set; }
    
    public string displayName { get; set; }
    
    public List<PermissionType> permissions { get; set; }

    public bool belongsToUser;
}