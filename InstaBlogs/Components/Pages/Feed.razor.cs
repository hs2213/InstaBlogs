using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;
using InstaBlogs.Services.Blogs;
using Microsoft.AspNetCore.Components;

namespace InstaBlogs.Components.Pages;

public partial class Feed
{
    [Inject]
    private IBlogService BlogService { get; set; } = default!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    private List<Blog> _blogs = new();
    
    private int _blogIndex = 0;
    
    private bool _blogsRendered = false;

    protected override void OnInitialized()
    {
        if (NavigationManager.Uri.Contains("approve"))
        {
            _blogs = BlogService.GetByStatus(Status.Pending).ToList();
            return;
        }
        
        _blogs = BlogService.GetRandomBlogs(20).ToList();
    }

    private void OnBlogClicked()
    {
        NavigationManager.NavigateTo($"/view/{_blogs[_blogIndex].Id}");
    }
    
    private void OnArrowUpPressed()
    {
        if (_blogIndex > 0)
        {
            _blogIndex--;
        }
    }
    
    private void OnArrowDownPressed()
    {
        ShouldReelsBeAdded();
        
        if (_blogs.Count == _blogIndex + 1)
        {
            return;
        }
        
        _blogIndex++;
    }

    private void ShouldReelsBeAdded()
    {
        if (NavigationManager.Uri.Contains("approve"))
        {
            return;
        }
        
        if ((_blogIndex + 1) % 20 == 0)
        {
            _blogs.AddRange(BlogService.GetRandomBlogs(20));
        }
    }
}