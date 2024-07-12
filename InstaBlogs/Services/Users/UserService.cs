using FluentValidation;
using FluentValidation.Results;
using InstaBlogs.Entities;
using InstaBlogs.Repositories.Users;
using InstaBlogs.Services.Notifications;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace InstaBlogs.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;
    private readonly INotificationService _notificationService;
    private readonly ProtectedSessionStorage _protectedSessionStorage;
    
    public UserService(
        IUserRepository userRepository,
        IValidator<User> userValidator,
        INotificationService notificationService, 
        ProtectedSessionStorage protectedSessionStorage)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _notificationService = notificationService;
        _protectedSessionStorage = protectedSessionStorage;
    }

    public async Task<bool> CheckIfExists(string email, CancellationToken cancellationToken = default)
    {
        User? user = await _userRepository.GetById(email, cancellationToken);

        if (user != null)
        {
            await _protectedSessionStorage.SetAsync(Constants.UserKey, user);
        }
        
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
            await _notificationService.ShowNotification("Invalid Data Provided");
            return;
        }
        
        await _userRepository.Create(user, cancellationToken);
        
        await _protectedSessionStorage.SetAsync(Constants.UserKey, user);
        
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