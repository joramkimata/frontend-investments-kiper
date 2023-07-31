using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace INKIPER.Dtos;


[JsonObject]
public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}