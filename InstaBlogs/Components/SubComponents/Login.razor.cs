using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace InstaBlogs.Components.SubComponents;

public partial class Login
{
    [Inject]
    private NavigationManager _navigationManager { get; set; } = default!;

    [CascadingParameter] protected Task<AuthenticationState> AuthState { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        ClaimsPrincipal user = (await AuthState).User;
        
        var hello = user!.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if(user.Identity?.IsAuthenticated == false)
        {
            _navigationManager.NavigateTo("Account/Login?redirectUri=feed");
        }
        
    }
    
    private void LogOut()
    {
        _navigationManager.NavigateTo("Account/Logout");
    }
}