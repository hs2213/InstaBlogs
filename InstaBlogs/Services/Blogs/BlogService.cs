using FluentValidation;
using FluentValidation.Results;
using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;
using InstaBlogs.Repositories.Blogs;
using InstaBlogs.Services.Comments;
using InstaBlogs.Services.Notifications;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace InstaBlogs.Services.Blogs;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly INotificationService _notificationService;
    private readonly IValidator<Blog> _blogValidator;
    private readonly ProtectedSessionStorage _sessionStorage;
    
    public BlogService(
        IBlogRepository blogRepository, 
        INotificationService notificationService,
        IValidator<Blog> blogValidator, 
        ProtectedSessionStorage sessionStorage)
    {
        _blogRepository = blogRepository;
        _notificationService = notificationService;
        _blogValidator = blogValidator;
        _sessionStorage = sessionStorage;
    }

    public async Task Create(Blog blog, CancellationToken cancellationToken = default)
    {
        blog.Id = Guid.NewGuid();
        blog.Created = DateTimeOffset.Now;

        ProtectedBrowserStorageResult<User> userInfo = await _sessionStorage.GetAsync<User>(Constants.UserKey);
        blog.UserEmail = userInfo.Value?.Email ?? string.Empty;
        
        ValidationResult validationResult = await _blogValidator.ValidateAsync(blog, cancellationToken);
        
        if (validationResult.IsValid == false)
        {
            await _notificationService.ShowNotification("Please ensure all fields are filled correctly");
            return;
        }
        
        await _blogRepository.Create(blog, cancellationToken);
        
        await _notificationService.ShowNotification("Blog Successfully Created");
    }
    
    public ValueTask<Blog?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return _blogRepository.GetById(id, cancellationToken);
    }
    
    public ICollection<Blog> GetByUserId(string id)
    {
        return _blogRepository.GetByUserId(id);
    }
    
    public ICollection<Blog> GetByStatus(Status status)
    {
        return _blogRepository.GetByStatus(status);
    }
    
    public ICollection<Blog> GetRandomBlogs(int noOfBlogs)
    {
        return _blogRepository.GetRandomBlogs(noOfBlogs);
    }
    
    public async ValueTask Update(Blog updatedBlog, CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await _blogValidator.ValidateAsync(updatedBlog, cancellationToken);
        
        if (validationResult.IsValid == false)
        {
            return;
        }
        
        await _blogRepository.Update(updatedBlog, cancellationToken);
        
        await _notificationService.ShowNotification("Blog Successfully Updated");
    }
    
    public async ValueTask Delete(Blog blog, CancellationToken cancellationToken = default)
    {
        await _blogRepository.Delete(blog, cancellationToken);
        
        await _notificationService.ShowNotification("Blog Successfully Deleted");
    }
}