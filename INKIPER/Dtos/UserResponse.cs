using System.Security.Claims;
using System.Text.Json.Serialization;
using INKIPER.Utils;

namespace INKIPER.Dtos;

public class UserResponse
{
    [JsonPropertyName("fullName")] public string FullName { get; set; }

    public List<String> Permissions { get; set; }
    
    public string AccessToken { get; set; }

    public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.Name, FullName)
        }.Concat(Permissions.Select(r => new Claim(ClaimTypes.Role, r)).ToArray()),
        Constants.AUTH_TYPE));

    public static UserResponse FromClaimsPrincipal(ClaimsPrincipal principal) => new()
    {
        FullName = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
        Permissions = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
    };
}