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
    
    public async Task Create(Comment comment, CancellationToken cancellationToken = default)
    {
        await _dbContext.Comments.AddAsync(comment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Comment?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Comments.FindAsync(id, cancellationToken);
    }
    
    public ICollection<Comment> GetByBlogId(Guid blogId)
    {
        return _dbContext.Comments.Where(comment => comment.BlogId == blogId).ToList();
    }
    
    public async Task Update(Comment updatedComment, CancellationToken cancellationToken = default)
    {
        _dbContext.Comments.Update(updatedComment);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task Delete(Comment comment, CancellationToken cancellationToken = default)
    {
        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}