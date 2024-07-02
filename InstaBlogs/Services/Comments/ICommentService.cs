using InstaBlogs.Entities;

namespace InstaBlogs.Services.Comments;

public interface ICommentService
{
    /// <summary>
    /// Creates a comment in the database.
    /// </summary>
    /// <param name="comment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CreateComment(Comment comment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a comment by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<Comment?> GetCommentById(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all the comments for a blog.
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    ICollection<Comment> GetCommentsByBlogId(Guid blogId);

    /// <summary>
    /// Updates an existing comment in the database.
    /// </summary>
    /// <param name="updatedComment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask UpdateComment(Comment updatedComment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a comment from the database.
    /// </summary>
    /// <param name="comment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask DeleteComment(Comment comment, CancellationToken cancellationToken = default);
}