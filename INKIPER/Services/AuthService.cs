using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using INKIPER.Dtos;
using INKIPER.Utils;
using Newtonsoft.Json.Linq;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace INKIPER.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserResponse?> Login(LoginDto loginDto)
    {
        UserResponse? user = null;
        string uuid = "";
        var response = await _httpClient.PostAsJsonAsync(Constants.LOGIN_URL, loginDto);
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return null;
        }

        var tokenResponse = response.Content.ReadFromJsonAsync<TokenResponse>().Result;
        
        

        var token = new JwtSecurityToken(jwtEncodedString: tokenResponse.AccessToken);
        
        var userClaim = token.Claims.FirstOrDefault(c => c.Type == "user");
        if (userClaim != null)
        {
            JObject userObject = JObject.Parse(userClaim.Value);
            user = userObject.ToObject<UserResponse>();
            uuid = user.Uuid;
        }
        
        var permissionsClaim = token.Claims.FirstOrDefault(c => c.Type == "permissions");
        
        if (permissionsClaim != null)
        {
            string[] permissions = permissionsClaim.Value.Split(',');
            List<String> Permissions = new();
            foreach (var permission in permissions)
            {
                Permissions.Add(permission);
            }
            user!.Permissions = Permissions;
        }
        
        user.AccessToken = tokenResponse.AccessToken;
        user.Uuid = uuid;
        
        
        return user;
    }
}