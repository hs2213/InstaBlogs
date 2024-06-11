using Microsoft.AspNetCore.Components;

namespace InstaBlogs.Components.Layout;

public partial class Navigation : ComponentBase
{
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;

}