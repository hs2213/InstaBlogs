using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;

namespace InstaBlogs.Repositories.Blogs;

public interface IBlogRepository
{
    /// <summary>
    /// Adds a blog to the database.
    /// </summary>
    /// <param name="blog"></param>
    /// <returns></returns>
    Task Create(Blog blog);

    /// <summary>
    /// Gets a blog by its ID from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<Blog?> GetById(Guid id);

    /// <summary>
    /// Gets all the blogs a specific user has created.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    ICollection<Blog> GetByUserEmail(string email);

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
    ICollection<Blog> GetRandomBlogs(int noOfBlogs);

    /// <summary>
    /// Updates an existing blog in the database.
    /// </summary>
    /// <param name="updatedBlog"></param>
    /// <returns></returns>
    ValueTask Update(Blog updatedBlog);

    /// <summary>
    /// Deletes a blog from the database.
    /// </summary>
    /// <param name="blog"></param>
    /// <returns></returns>
    ValueTask Delete(Blog blog);
}