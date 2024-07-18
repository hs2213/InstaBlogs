using InstaBlogs.Entities;

namespace InstaBlogs.Repositories.Comments;

public interface ICommentRepository
{
    /// <summary>
    /// Creates a comment in the database.
    /// </summary>
    /// <param name="comment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Create(Comment comment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a comment by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Comment?> GetById(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets all the comments for a blog.
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    ICollection<Comment> GetByBlogId(Guid blogId);

    /// <summary>
    /// Updates an existing comment in the database.
    /// </summary>
    /// <param name="updatedComment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Update(Comment updatedComment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a comment from the database.
    /// </summary>
    /// <param name="comment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Delete(Comment comment, CancellationToken cancellationToken = default);
}