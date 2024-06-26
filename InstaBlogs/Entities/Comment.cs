namespace InstaBlogs.Entities;

public class Comment
{
    public Guid Id { get; set; }
    
    public Guid BlogId { get; set; }
    
    public required string UserEmail { get; set; }
    
    public required string Content { get; set; }
}