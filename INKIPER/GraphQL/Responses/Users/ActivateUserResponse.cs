using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Users;

public class ActivateUserResponse
{
    public MutationResponse<UserType> activateUser { get; set; }
}