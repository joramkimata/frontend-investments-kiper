using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Users;

public class ChangeUserPasswordResponse
{
    public MutationResponse<UserType> changeUserPassword { get; set; }
}