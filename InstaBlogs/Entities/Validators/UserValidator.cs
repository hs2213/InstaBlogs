using FluentValidation;

namespace InstaBlogs.Entities.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(user => user.Role)
            .IsInEnum().WithMessage("Role is not valid");
    }
}