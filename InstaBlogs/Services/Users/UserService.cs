using FluentValidation;
using FluentValidation.Results;
using InstaBlogs.Entities;
using InstaBlogs.Repositories.Users;
using InstaBlogs.Services.Auth0;
using InstaBlogs.Services.Notifications;

namespace InstaBlogs.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;
    private readonly IAuth0Service _authService;
    private readonly INotificationService _notificationService;
    
    public UserService(
        IUserRepository userRepository,
        IValidator<User> userValidator,
        IAuth0Service authService,
        INotificationService notificationService)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _authService = authService;
        _notificationService = notificationService;
    }

    public async Task<bool> CheckIfExists(string email, CancellationToken cancellationToken = default)
    {
        User? user = await _userRepository.GetById(email, cancellationToken);
        
        return user != null;
    }
    
    public ValueTask<User?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        return _userRepository.GetById(email, cancellationToken);
    }
    
    public async Task Create(User user, CancellationToken cancellationToken = default)
    {
        ValidationResult? validationResult = await _userValidator.ValidateAsync(user, cancellationToken);

        if (validationResult?.IsValid == false)
        {
            return;
        }
        
        await _userRepository.Create(user, cancellationToken);

        await _authService.AssignRole(user);

        await _notificationService.ShowNotification("Created account successfully");
    }
    
    public async ValueTask Update(User updatedUser, CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await _userValidator.ValidateAsync(updatedUser, cancellationToken);

        if (validationResult?.IsValid == false)
        {
            return;
        }
        
        await _userRepository.Update(updatedUser, cancellationToken);
        
        await _notificationService.ShowNotification("Updated account successfully");
    }
}