using FluentValidation;
using FluentValidation.Results;
using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;
using InstaBlogs.Repositories.Blogs;
using InstaBlogs.Services.Comments;
using InstaBlogs.Services.Notifications;

namespace InstaBlogs.Services.Blogs;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly INotificationService _notificationService;
    private readonly IValidator<Blog> _blogValidator;
    
    public BlogService(
        IBlogRepository blogRepository, 
        INotificationService notificationService,
        IValidator<Blog> blogValidator)
    {
        _blogRepository = blogRepository;
        _notificationService = notificationService;
        _blogValidator = blogValidator;
    }

    public async Task Create(Blog blog, CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await _blogValidator.ValidateAsync(blog, cancellationToken);
        
        if (validationResult.IsValid == false)
        {
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