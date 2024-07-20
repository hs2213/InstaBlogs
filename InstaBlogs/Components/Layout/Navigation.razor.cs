using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace InstaBlogs.Components.Layout;

public partial class Navigation : ComponentBase, IDisposable
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += RefreshState;
    }

    private void RefreshState(object? sender, LocationChangedEventArgs e)
    {
        StateHasChanged();
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= RefreshState;
    }
}