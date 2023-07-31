using System.Security.Claims;
using INKIPER.Dtos;
using Microsoft.AspNetCore.Components.Authorization;

namespace INKIPER.Auth;

public class MyAuthenticationStateProvider: AuthenticationStateProvider
{
    private readonly MyUserService _myUserService;
    
    public UserResponse CurrentUser { get; private set; } = new();

    public MyAuthenticationStateProvider(MyUserService myUserService)
    {
        _myUserService = myUserService;
        AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();
        var user = await _myUserService.FetchUserFromBrowserAsync();

        if (user is not null)
        {
            var authenticatedUser = await _myUserService.FetchUserFromBrowserAsync();
            CurrentUser = authenticatedUser;
            
            if (authenticatedUser is not null)
            {
                principal = authenticatedUser.ToClaimsPrincipal();
            }
        }

        return new(principal);
    }
    
    public async  Task<UserResponse?> LoginAsync(string username, string password)
    {
        var principal = new ClaimsPrincipal();
        var user = await _myUserService.FindUserFromApiAsync(username, password);
        
        CurrentUser = user;
        
        if (user is not null)
        {
            principal = user.ToClaimsPrincipal();
        }
        else
        {
            return null;
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));

        return user;
    }
    
    public async Task LogoutAsync()
    {
        await _myUserService.ClearBrowserUserDataAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
    }
    
    private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
    {
        var authenticationState = await task;

        CurrentUser = UserResponse.FromClaimsPrincipal(authenticationState.User);
    }
    
    public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
    
    
}