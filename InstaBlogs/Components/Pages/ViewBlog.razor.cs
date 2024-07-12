using InstaBlogs.Entities;
using InstaBlogs.Services.Blogs;
using Microsoft.AspNetCore.Components;

namespace InstaBlogs.Components.Pages;

public partial class ViewBlog : ComponentBase
{
    [Parameter]
    public Guid Id { get; set; }
    
    [Inject]
    private IBlogService BlogService { get; set; } = default!;
    
    private Blog? _displayedBlog;
    
    private string _htmlContent = string.Empty;
    
    protected override async Task OnInitializedAsync()
    {
        if (Id == Guid.Empty)
        {
            return;
        }
        
        _displayedBlog = await BlogService.GetById(Id);
        
        if (_displayedBlog == null)
        {
            return;
        }
        
        _htmlContent = Markdig.Markdown.ToHtml(_displayedBlog.Content);
    }
}