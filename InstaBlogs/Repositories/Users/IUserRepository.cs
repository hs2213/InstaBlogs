using InstaBlogs.Entities;

namespace InstaBlogs.Repositories.Users;

public interface IUserRepository
{
    /// <summary>
    /// Creates a user in the database.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Create(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user by their email.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    User? GetById(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user in the database.
    /// </summary>
    /// <param name="updatedUser"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Update(User updatedUser, CancellationToken cancellationToken = default);
}