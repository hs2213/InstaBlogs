using System.ComponentModel.DataAnnotations;
using InstaBlogs.Entities.Enums;

namespace InstaBlogs.Entities;

public class User
{
    [Key]
    public required string Id { get; set; }
    
    public required string Email { get; set; }
    
    public Role Role { get; set; }
    
    public required string Name { get; set; }
    
    public string ProfilePictureLink { get; set; } = string.Empty;
}