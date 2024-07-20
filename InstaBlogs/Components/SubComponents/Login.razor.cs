using System.Security.Claims;
using InstaBlogs.Entities;
using InstaBlogs.Services.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace InstaBlogs.Components.SubComponents;

public partial class Login
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    [Inject]
    private IUserService UserService { get; set; } = default!;

    [Inject]
    private ProtectedSessionStorage ProtectedSessionStorage { get; set; } = default!;
    
    [CascadingParameter] protected Task<AuthenticationState> AuthState { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender == false)
        {
            return;
        }

        await CheckUserAuthenticatedAndInDb();
    }

    private async Task CheckUserAuthenticatedAndInDb()
    {
        var result = await ProtectedSessionStorage.GetAsync<User>(Constants.UserKey);

        if (result.Success)
        {
            return;
        }
        
        ClaimsPrincipal user = (await AuthState).User;
        
        if(user.Identity?.IsAuthenticated == false)
        {
            NavigationManager.NavigateTo("Account/Login?redirectUri=", true);
            return;
        }
        
        string? userId = user!.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        bool userExists = await UserService.CheckIfExists(userId!);

        if (userExists == false)
        {
            NavigationManager.NavigateTo("register");
        }
    }

    private async Task LogOut()
    {
        await ProtectedSessionStorage.DeleteAsync(Constants.UserKey);
        
        NavigationManager.NavigateTo("Account/Logout");
    }
}