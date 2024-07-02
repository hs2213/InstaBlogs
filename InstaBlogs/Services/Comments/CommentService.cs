using FluentValidation;
using FluentValidation.Results;
using InstaBlogs.Entities;
using InstaBlogs.Repositories.Comments;
using InstaBlogs.Services.Notifications;

namespace InstaBlogs.Services.Comments;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<Comment> _commentValidator;
    private readonly INotificationService _notificationService;

    public CommentService(
        ICommentRepository commentRepository,
        IValidator<Comment> commentValidator,
        INotificationService notificationService)
    {
        _commentRepository = commentRepository;
        _commentValidator = commentValidator;
        _notificationService = notificationService;
    }
    
    public async Task CreateComment(Comment comment, CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await _commentValidator.ValidateAsync(comment, cancellationToken);

        if (validationResult.IsValid == false)
        {
            return;
        }
        
        await _commentRepository.Create(comment, cancellationToken);
        
        await _notificationService.ShowNotification("Comment Created");
    }
    
    public ValueTask<Comment?> GetCommentById(Guid id, CancellationToken cancellationToken = default)
    {
        return  _commentRepository.GetById(id, cancellationToken);
    }
    
    public ICollection<Comment> GetCommentsByBlogId(Guid blogId)
    {
        return _commentRepository.GetByBlogId(blogId);
    }
    
    public async ValueTask UpdateComment(Comment updatedComment, CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await _commentValidator.ValidateAsync(updatedComment, cancellationToken);

        if (validationResult.IsValid == false)
        {
            return;
        }
        
        await _commentRepository.Update(updatedComment, cancellationToken);
        
        await _notificationService.ShowNotification("Comment Updated");
    }
    
    public async ValueTask DeleteComment(Comment comment, CancellationToken cancellationToken = default)
    {
        await _commentRepository.Delete(comment, cancellationToken);
        
        await _notificationService.ShowNotification("Comment Deleted");
    }
}