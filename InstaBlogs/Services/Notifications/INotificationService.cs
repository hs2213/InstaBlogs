using InstaBlogs.Components.SubComponents;

namespace InstaBlogs.Services.Notifications;

public interface INotificationService
{
    /// <summary>
    /// Component to show message on
    /// </summary>
    public Notification? NotificationComponent { get; set; }

    /// <summary>
    /// Causes the <see cref="NotificationComponent"/> to render with the message given.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task ShowNotification(string message);
}