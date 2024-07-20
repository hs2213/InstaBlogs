using System.Collections;
using InstaBlogs.DBContext;
using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;
using Microsoft.EntityFrameworkCore;

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
    
    public IEnumerable<Blog> GetByTitle(string title, CancellationToken cancellationToken = default)
    {
        return _dbContext.Blogs.Where(blog => blog.Title.Contains(title)).ToList();
    }
    
    public async Task<Blog?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Blogs.FindAsync(id, cancellationToken);
    }
    
    public ICollection<Blog> GetByUserId(string id)
    {
        return _dbContext.Blogs.Where(blog => blog.UserId == id).ToList();
    }
    
    public ICollection<Blog> GetByStatus(Status status)
    {
        return _dbContext.Blogs.Where(blog => blog.Status == status).ToList();
    }
    
    public ICollection<Blog> GetRandomAuthorisedBlogs(int noOfBlogs)
    {
        return _dbContext.Blogs
            .AsEnumerable()
            .Where(blog => blog.Status == Status.Approved)
            .OrderBy(t => Guid.NewGuid())
            .Take(noOfBlogs)
            .ToList();
    }
    
    public async Task Update(Blog updatedBlog, CancellationToken cancellationToken = default)
    {
        _dbContext.Blogs.Update(updatedBlog);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task Delete(Blog blog, CancellationToken cancellationToken = default)
    {
        _dbContext.Blogs.Remove(blog);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}