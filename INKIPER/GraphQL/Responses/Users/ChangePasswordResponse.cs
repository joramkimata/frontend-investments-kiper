using System.Text.Json.Serialization;
using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Users;

public class ChangePasswordResponse
{
    [JsonPropertyName("changeUserPassword")]
    public MutationResponse<UserType> changeUserPassword { get; set; }
}

