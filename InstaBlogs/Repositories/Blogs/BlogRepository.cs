using System.Collections;
using InstaBlogs.DBContext;
using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;

namespace InstaBlogs.Repositories.Blogs;

public class BlogRepository : IBlogRepository
{
    private readonly InstaBlogsDBContext _dbContext;
    
    public BlogRepository(InstaBlogsDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Create(Blog blog)
    {
        await _dbContext.Blogs.AddAsync(blog);
        await _dbContext.SaveChangesAsync();
    }
    
    public ValueTask<Blog?> GetById(Guid id)
    {
        return _dbContext.Blogs.FindAsync(id);
    }
    
    public ICollection<Blog> GetByUserEmail(string email)
    {
        return _dbContext.Blogs.Where(blog => blog.UserEmail == email).ToList();
    }
    
    public ICollection<Blog> GetByStatus(Status status)
    {
        return _dbContext.Blogs.Where(blog => blog.Status == status).ToList();
    }
    
    public ICollection<Blog> GetRandomBlogs(int noOfBlogs)
    {
        return _dbContext.Blogs.OrderBy(blog => Guid.NewGuid()).Take(noOfBlogs).ToList();
    }
    
    public async ValueTask Update(Blog updatedBlog)
    {
        _dbContext.Blogs.Update(updatedBlog);
        await _dbContext.SaveChangesAsync();
    }
    
    public async ValueTask Delete(Blog blog)
    {
        _dbContext.Blogs.Remove(blog);
        await _dbContext.SaveChangesAsync();
    }
}