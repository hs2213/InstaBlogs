namespace InstaBlogs.Entities;

public class Comment
{
    public Guid Id { get; set; }
    
    public Guid BlogId { get; set; }
    
    public string UserEmail { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}