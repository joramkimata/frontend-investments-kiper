namespace INKIPER.GraphQL.Inputs.Users;

public class AssignRolesInput
{
    public required string? userUUID { get; set; }
    public required List<string> roleUUIDs { get; set; }
}