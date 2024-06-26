using InstaBlogs.DBContext;
using InstaBlogs.Entities;
using Microsoft.EntityFrameworkCore;

namespace InstaBlogs.Repositories.Comments;

public class CommentRepository : ICommentRepository
{
    private readonly InstaBlogsDBContext _dbContext;
    
    public CommentRepository(InstaBlogsDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Create(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }
    
    public ValueTask<Comment?> GetById(Guid id)
    {
        return _dbContext.Comments.FindAsync(id);
    }
    
    public ICollection<Comment> GetByBlogId(Guid blogId)
    {
        return _dbContext.Comments.Where(comment => comment.BlogId == blogId).ToList();
    }
    
    public async ValueTask Update(Comment updatedComment)
    {
        _dbContext.Comments.Update(updatedComment);
        await _dbContext.SaveChangesAsync();
    }
    
    public async ValueTask Delete(Comment comment)
    {
        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();
    }
}