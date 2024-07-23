using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;
using InstaBlogs.Services.Blogs;
using Microsoft.AspNetCore.Components;

namespace InstaBlogs.Components.SubComponents;

public partial class Feed
{
    [Inject]
    private IBlogService BlogService { get; set; } = default!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    private List<Blog> _blogs = new();

    private Blog _blogToShow = new Blog();
    
    private Reel _reel = default!;
    
    private int _blogIndex = 0;
    
    protected override void OnInitialized()
    {
        if (NavigationManager.Uri.Contains("approve"))
        {
            _blogs = BlogService.GetByStatus(Status.Pending).ToList();
            return;
        }
        
        _blogs = BlogService.GetRandomBlogs(20).ToList();
        
        _blogToShow = _blogs[_blogIndex];
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
        
        _blogToShow = _blogs[_blogIndex];
        
        StateHasChanged();
        
        _reel.ConvertContent(_blogToShow);
    }
    
    private void OnArrowDownPressed()
    {
        ShouldReelsBeAdded();
        
        if (_blogs.Count == _blogIndex + 1)
        {
            return;
        }
        
        _blogIndex++;
        
        _blogToShow = _blogs[_blogIndex];
        
        StateHasChanged();
        
        _reel.ConvertContent(_blogToShow);
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