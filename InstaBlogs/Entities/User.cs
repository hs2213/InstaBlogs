using System.ComponentModel.DataAnnotations;
using InstaBlogs.Entities.Enums;

namespace InstaBlogs.Entities;

public class User
{
    [Key] 
    public string Id { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    
    public Role Role { get; set; }

    public string Name { get; set; } = string.Empty;
}