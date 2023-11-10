using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Users;

public class BlockUserResponse
{
    public MutationResponse<UserType> blockUser { get; set; }
}