using InstaBlogs.Entities;

namespace InstaBlogs.Repositories.Users;

public interface IUserRepository
{
    /// <summary>
    /// Creates a user in the database.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task Create(User user);

    /// <summary>
    /// Gets a user by their email.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    ValueTask<User?> GetByEmail(string email);

    /// <summary>
    /// Updates an existing user in the database.
    /// </summary>
    /// <param name="updatedUser"></param>
    /// <returns></returns>
    ValueTask Update(User updatedUser);
}