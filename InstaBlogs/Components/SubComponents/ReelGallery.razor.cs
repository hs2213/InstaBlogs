using InstaBlogs.Entities;
using Microsoft.AspNetCore.Components;

namespace InstaBlogs.Components.SubComponents;

public partial class ReelGallery
{
    [Parameter]
    public List<Blog> Blogs { get; set; } = new List<Blog>();
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    private void NavigateToBlog(Guid blogId)
    {
        NavigationManager.NavigateTo($"view/{blogId}");
    }
}