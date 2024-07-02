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
    
    public async Task Create(User user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public ValueTask<User?> GetById(string id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users.FindAsync(id, cancellationToken);
    }

    public async ValueTask Update(User updatedUser, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Update(updatedUser);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}