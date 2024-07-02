using FluentValidation;
using InstaBlogs.Entities;
using InstaBlogs.Entities.Validators;
using InstaBlogs.Repositories.Blogs;
using InstaBlogs.Repositories.Comments;
using InstaBlogs.Repositories.Users;
using InstaBlogs.Services.Blogs;
using InstaBlogs.Services.Comments;
using InstaBlogs.Services.Notifications;
using InstaBlogs.Services.Users;

namespace InstaBlogs.Extensions;

public static class ServiceExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IBlogRepository, BlogRepository>()
            .AddScoped<ICommentRepository, CommentRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IBlogService, BlogService>()
            .AddScoped<ICommentService, CommentService>()
            .AddSingleton<INotificationService, NotificationService>();
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<User>, UserValidator>()
            .AddScoped<IValidator<Blog>, BlogValidator>()
            .AddScoped<IValidator<Comment>, CommentValidator>();
    }
}