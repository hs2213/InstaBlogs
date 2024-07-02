using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;

namespace InstaBlogs.Services.Blogs;

public interface IBlogService
{
    /// <summary>
    /// Creates a blog in the database.
    /// </summary>
    /// <param name="blog"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Create(Blog blog, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a blog by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<Blog?> GetById(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets all the blogs for a user.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ICollection<Blog> GetByUserId(string id);

    /// <summary>
    /// Gets blogs by a specific status.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    ICollection<Blog> GetByStatus(Status status);

    /// <summary>
    /// Gets a specified number of blogs randomly.
    /// </summary>
    /// <param name="noOfBlogs"></param>
    /// <returns></returns>
    ICollection<Blog> GetRandomBlogs(int noOfBlogs);

    /// <summary>
    /// Updates an existing blog in the database.
    /// </summary>
    /// <param name="updatedBlog"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask Update(Blog updatedBlog, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an existing blog in the database.
    /// </summary>
    /// <param name="blog"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask Delete(Blog blog, CancellationToken cancellationToken = default);
}