using InstaBlogs.Entities;
using InstaBlogs.Services.Blogs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace InstaBlogs.Components.Pages;

public partial class Profile : ComponentBase
{
    private User _displayedUser = new User();

    private List<Blog> _blogs = new List<Blog>();
    
    [Inject]
    private ProtectedSessionStorage ProtectedSessionStorage { get; set; } = default!;

    [Inject]
    private IBlogService BlogService { get; set; } = default!;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender == false)
        {
            return;
        }
        
        ProtectedBrowserStorageResult<User> activeUser = await ProtectedSessionStorage.GetAsync<User>(Keys.UserKey);

        if (activeUser.Success == false)
        {
            return;
        }
        
        _displayedUser = activeUser!.Value ?? new User();
        
        _blogs = BlogService.GetByUserId(_displayedUser.Id).ToList();
        
        StateHasChanged();
    }
}