using System.ComponentModel.DataAnnotations;

namespace InstaBlogs.Entities;

public class Comment
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid BlogId { get; set; }
    
    public string UserId { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}