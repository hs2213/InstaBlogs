using InstaBlogs.DBContext;
using InstaBlogs.Entities;

namespace InstaBlogs.Repositories.Users;

public class UserRepository : IUserRepository, IAsyncDisposable
{
    private readonly InstaBlogsDBContext _dbContext;

    public UserRepository(InstaBlogsDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Create(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public ValueTask<User?> GetByEmail(string email)
    {
        return _dbContext.Users.FindAsync(email);
    }

    public async ValueTask Update(User updatedUser)
    {
        _dbContext.Users.Update(updatedUser);
        await _dbContext.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}