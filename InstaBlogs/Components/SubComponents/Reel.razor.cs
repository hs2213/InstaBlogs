using InstaBlogs.Entities;
using Microsoft.AspNetCore.Components;

namespace InstaBlogs.Components.SubComponents;

public partial class Reel : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    [Parameter]
    public Blog BlogToShow { get; set; } = default!;
    
    private string _content = string.Empty;

    [Parameter]
    public EventCallback OnClicked { get; set; }
    
    private async Task ReelClicked()
    {
        await OnClicked.InvokeAsync();
    }
    
    protected override void OnInitialized()
    {
        ConvertContent();
    }

    public void ConvertContent()
    {
        _content = Markdig.Markdown.ToHtml(BlogToShow.Content);
    }
}