using InstaBlogs.Entities.Enums;

namespace InstaBlogs.Entities;

public class Blog
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
    
    public DateTimeOffset Created { get; set; }

    public Status Status { get; set; } = Status.Pending;

    public bool CommentsEnabled { get; set; } = true;
}