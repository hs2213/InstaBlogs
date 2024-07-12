using System.Security.Claims;
using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;
using InstaBlogs.Services.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace InstaBlogs.Components.Pages;

public partial class Register
{
    [Inject]
    private IUserService UserService { get; set; } = default!;
    
    private readonly User _userToRegister = new User();
    
    [CascadingParameter] 
    protected Task<AuthenticationState> AuthState { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender == false)
        {
            return;
        }
        
        AuthenticationState authState = await AuthState;

        if (authState.User.Identity?.IsAuthenticated == false)
        {
            return;
        }
        
        _userToRegister.Id = authState.User!.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
    }

    private async Task RegisterUser()
    {
        await UserService.Create(_userToRegister);
    }

    private void RefreshRole(ChangeEventArgs args)
    {
        if (args.Value == null)
        {
            return;
        }
        
        _userToRegister.Role = Enum.Parse<Role>(args.Value!.ToString()!);
    }
}