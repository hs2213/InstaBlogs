using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;

namespace InstaBlogs.Repositories.Blogs;

public interface IBlogRepository
{
    /// <summary>
    /// Adds a blog to the database.
    /// </summary>
    /// <param name="blog"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Create(Blog blog, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets blogs by their title from the database.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    IEnumerable<Blog> GetByTitle(string title, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a blog by its ID from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Blog?> GetById(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all the blogs a specific user has created.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ICollection<Blog> GetByUserId(string id);

    /// <summary>
    /// Gets all the blogs with a 
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    ICollection<Blog> GetByStatus(Status status);

    /// <summary>
    /// Gets a random number of blogs from the database.
    /// </summary>
    /// <param name="noOfBlogs">Number of blogs to return</param>
    /// <returns></returns>
    ICollection<Blog> GetRandomAuthorisedBlogs(int noOfBlogs);

    /// <summary>
    /// Updates an existing blog in the database.
    /// </summary>
    /// <param name="updatedBlog"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Update(Blog updatedBlog, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a blog from the database.
    /// </summary>
    /// <param name="blog"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Delete(Blog blog, CancellationToken cancellationToken = default);
}