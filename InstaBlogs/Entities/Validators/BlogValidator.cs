using FluentValidation;

namespace InstaBlogs.Entities.Validators;

public class BlogValidator : AbstractValidator<Blog>
{
    public BlogValidator()
    {
        RuleFor(blog => blog.Id)
            .NotEqual(Guid.Empty).NotEmpty().WithMessage("Id is required");
        
        RuleFor(blog => blog.Title)
            .NotEmpty().WithMessage("Title is required");
        
        RuleFor(blog => blog.Content)
            .NotEmpty().WithMessage("Content is required");
        
        RuleFor(blog => blog.UserEmail)
            .NotEmpty().WithMessage("User Email is required");
        
        RuleFor(blog => blog.Created)
            .NotEmpty().WithMessage("Created Date and Time is required");
        
        RuleFor(blog => blog.Status)
            .IsInEnum().WithMessage("Status is not valid");
    }
}