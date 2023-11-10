namespace INKIPER.GraphQL.Inputs.Roles;

public class AssignPermissionsInput
{
    public required string roleUUID { get; set; }
    public required List<string> permissionUUIDs { get; set; }
}