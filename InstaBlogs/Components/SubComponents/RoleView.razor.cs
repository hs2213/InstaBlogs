using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace InstaBlogs.Components.SubComponents;

public partial class RoleView
{
    [Parameter] 
    public IEnumerable<Role> RoleToDisplay { get; set; } = [];
    
    [Parameter]
    public RenderFragment? WrongRoleContent { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Inject] 
    private ProtectedSessionStorage ProtectedSessionStorage { get; set; } = default!;

    private bool _isContentRestricted = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender == false)
        {
            return;
        }
        
        ProtectedBrowserStorageResult<User> activeUser = await ProtectedSessionStorage.GetAsync<User>(Constants.UserKey);

        if (activeUser.Success == false)
        {
            return;
        }

        _isContentRestricted = RoleToDisplay.Contains<Role>(activeUser.Value!.Role) == false;
        
        StateHasChanged();
    }
}