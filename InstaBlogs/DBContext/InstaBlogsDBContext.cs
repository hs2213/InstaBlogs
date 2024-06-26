using InstaBlogs.Entities;
using Microsoft.EntityFrameworkCore;

namespace InstaBlogs.DBContext;

public class InstaBlogsDBContext : DbContext
{ 
    public InstaBlogsDBContext(DbContextOptions<InstaBlogsDBContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Blog> Blogs { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
}