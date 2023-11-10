using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Users;

public class AssignRolesResponse
{
    public MutationResponse<UserType> assignRoles { get; set; }
}