using FluentValidation;

namespace InstaBlogs.Entities.Validators;

public class CommentValidator : AbstractValidator<Comment>
{
    public CommentValidator()
    {
        RuleFor(comment => comment.Id)
            .NotEqual(Guid.Empty).NotEmpty().WithMessage("Id is required");
        
        RuleFor(comment => comment.BlogId)
            .NotEqual(Guid.Empty).NotEmpty().WithMessage("Blog Id is required");
        
        RuleFor(comment => comment.UserId)
            .NotEmpty().WithMessage("User Email is required");
        
        RuleFor(comment => comment.Content)
            .NotEmpty().WithMessage("Content is required");
    }
}