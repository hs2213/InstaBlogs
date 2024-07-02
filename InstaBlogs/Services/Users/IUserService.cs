using InstaBlogs.Entities;

namespace InstaBlogs.Services.Users;

public interface IUserService
{
    /// <summary>
    /// Checks if a user already exists in the db
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> CheckIfExists(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user by their email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<User?> GetByEmail(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a user and saves their role in auth0.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Create(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user in the database.
    /// </summary>
    /// <param name="updatedUser"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask Update(User updatedUser, CancellationToken cancellationToken = default);
}