using InstaBlogs.Components.SubComponents;

namespace InstaBlogs.Services.Notifications;

public class NotificationService : INotificationService
{
    public Notification? NotificationComponent { get; set; }

    public async Task ShowNotification(string message)
    {
        if (NotificationComponent == null)
        {
            return;
        }
    
        await NotificationComponent.Notify(message);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
    
}