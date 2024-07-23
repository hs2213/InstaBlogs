using InstaBlogs.Entities;
using InstaBlogs.Services.Blogs;
using InstaBlogs.Services.Notifications;
using Microsoft.AspNetCore.Components;

namespace InstaBlogs.Components.Pages;

public partial class Search : ComponentBase
{
    private string _searchTerm = string.Empty;

    private List<Blog> _searchResults = [];
    
    private bool _noResults = false;
    
    [Inject]
    private IBlogService BlogService { get; set; } = default!;
    
    [Inject]
    private INotificationService NotificationService { get; set; } = default!;

    private async Task SearchBlogs()
    {
        if (string.IsNullOrWhiteSpace(_searchTerm))
        {
            await NotificationService.ShowNotification("Search term cannot be empty");
        }
        
        _searchResults = BlogService.GetByTitle(_searchTerm).ToList();

        _noResults = _searchResults.Count == 0;
        
        StateHasChanged();
    }
}