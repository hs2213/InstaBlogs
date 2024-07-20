using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;
using InstaBlogs.Services.Blogs;
using InstaBlogs.Services.Comments;
using InstaBlogs.Services.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace InstaBlogs.Components.Pages;

public partial class ViewBlog : ComponentBase
{
    [Parameter]
    public Guid Id { get; set; }
    
    [Inject]
    private IBlogService BlogService { get; set; } = default!;
    
    [Inject]
    private ProtectedSessionStorage SessionStorage { get; set; } = default!;
    
    [Inject]
    private ICommentService CommentService { get; set; } = default!;
    
    [Inject]
    private IUserService UserService { get; set; } = default!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private List<CommentStructure> _blogComments = [];
    
    private Blog? _displayedBlog;
    
    private User _currentUser = new User();
    
    private Comment _newComment = new Comment();
    
    private string _userName = string.Empty;
    
    private string _htmlContent = string.Empty;
    
    private string _style = string.Empty;
    
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
        
        ChooseStyle();
        
        _userName = UserService.GetById(_displayedBlog.UserId)?.Name ?? string.Empty;

        GetCommentsWithUser();
    }

    private void GetCommentsWithUser()
    {
        _blogComments = CommentService.GetCommentsByBlogId(_displayedBlog?.Id ?? Guid.Empty)
            .Select(comment => new CommentStructure
            {
                Comment = comment,
                User = UserService.GetById(comment.UserId)
            }).ToList();
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender == false)
        {
            return;
        }
        
        ProtectedBrowserStorageResult<User> user = await SessionStorage.GetAsync<User>(Constants.UserKey);
        
        _currentUser = user.Value ?? new User();
    }

    private async Task AddComment()
    {
        if (_displayedBlog == null)
        {
            return;
        }
        
        _newComment.BlogId = _displayedBlog.Id;
        await CommentService.CreateComment(_newComment);
        
        _blogComments.Add(new CommentStructure{Comment = _newComment, User = _currentUser});
    }
    
    private async Task DeleteComment(CommentStructure comment)
    {
        await CommentService.DeleteComment(comment.Comment!);
        
        _blogComments.Remove(comment);
    }

    private async Task UpdateBlogStatus(Status statusGiven)
    {
        if (_displayedBlog == null)
        {
            return;
        }
        
        _displayedBlog.Status = statusGiven;
        await BlogService.Update(_displayedBlog);
        
        ChooseStyle();
    }

    private void ChooseStyle()
    {
        _style = _displayedBlog?.Status switch
        {
            Status.Pending => "background-color: rgba(255, 255, 159, 0.8)",
            Status.Approved => "background-color: rgba(201, 242, 155, 0.8);",
            Status.Rejected => "background-color: rgba(217, 30, 24, 0.8);",
            _ => string.Empty
        };
    }

    private async Task DeleteBlog()
    {
        if (_displayedBlog == null)
        {
            return;
        }
        
        await BlogService.Delete(_displayedBlog);
    }

    private void UpdateBlog()
    {
        NavigationManager.NavigateTo($"edit/{_displayedBlog?.Id}");
    }
}