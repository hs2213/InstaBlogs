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
    
    public async Task Create(Blog blog, CancellationToken cancellationToken = default)
    {
        await _dbContext.Blogs.AddAsync(blog, cancellationToken);
        await _dbContext.SaveChangesAsync();
    }
    
    public ValueTask<Blog?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Blogs.FindAsync(id, cancellationToken);
    }
    
    public ICollection<Blog> GetByUserId(string id)
    {
        return _dbContext.Blogs.Where(blog => blog.UserEmail == id).ToList();
    }
    
    public ICollection<Blog> GetByStatus(Status status)
    {
        return _dbContext.Blogs.Where(blog => blog.Status == status).ToList();
    }
    
    public ICollection<Blog> GetRandomBlogs(int noOfBlogs)
    {
        return _dbContext.Blogs.OrderBy(blog => Guid.NewGuid()).Take(noOfBlogs).ToList();
    }
    
    public async ValueTask Update(Blog updatedBlog, CancellationToken cancellationToken = default)
    {
        _dbContext.Blogs.Update(updatedBlog);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async ValueTask Delete(Blog blog, CancellationToken cancellationToken = default)
    {
        _dbContext.Blogs.Remove(blog);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}