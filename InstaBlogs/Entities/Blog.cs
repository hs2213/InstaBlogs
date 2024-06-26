using InstaBlogs.Entities.Enums;

namespace InstaBlogs.Entities;

public class Blog
{
    public Guid Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Content { get; set; }
    
    public required string UserEmail { get; set; }
    
    public DateTimeOffset Created { get; set; }
    
    public Status Status { get; set; }
}