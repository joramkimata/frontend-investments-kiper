using INKIPER.Dtos;
using INKIPER.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;

namespace INKIPER.Auth;

public class MyUserService
{
    private readonly ProtectedLocalStorage _protectedLocalStorage;
    private readonly AuthService _authService;
    private readonly string _storageKey = "STORAGE_KEY";

    public MyUserService(ProtectedLocalStorage protectedLocalStorage, AuthService authService)
    {
        _protectedLocalStorage = protectedLocalStorage;
        _authService = authService;
    }
    
    public async Task<UserResponse?> FindUserFromApiAsync(string username, string password)
    {
        var userResponse = await this._authService.Login(new LoginDto()
        {
            Email = username,
            Password = password
        });
        
        
        if (userResponse is not null)
        {
            await PersistUserToBrowserAsync(userResponse);
        }

        return userResponse;
    }

    private async Task PersistUserToBrowserAsync(UserResponse user)
    {
        string userJson = JsonConvert.SerializeObject(user);
        await _protectedLocalStorage.SetAsync(_storageKey, userJson);
    }
    
    public async Task<UserResponse?> FetchUserFromBrowserAsync()
    {
        try 
        {
            var fetchedUserResult = await _protectedLocalStorage.GetAsync<string>(_storageKey);

            if (fetchedUserResult.Success && !string.IsNullOrEmpty(fetchedUserResult.Value))
            {
                var user = JsonConvert.DeserializeObject<UserResponse>(fetchedUserResult.Value);

                return user;
            }
        }
        catch
        {
        }

        return null;
    }
    
    public async Task ClearBrowserUserDataAsync() => await _protectedLocalStorage.DeleteAsync(_storageKey);
}