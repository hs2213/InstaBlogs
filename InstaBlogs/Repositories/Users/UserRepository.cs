using InstaBlogs.DBContext;
using InstaBlogs.Entities;

namespace InstaBlogs.Repositories.Users;

public class UserRepository : IUserRepository
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
    
}