using InstaBlogs.Services.Notifications;
using Microsoft.AspNetCore.Components;

namespace InstaBlogs.Components.SubComponents;

public partial class Notification
{
    /// <summary>
    /// Used to assign component to the service.
    /// </summary>
    [Inject] 
    private INotificationService NotificationService { get; set; } = default!;

    // [Inject] 
    // private IStringLocalizer<Resources> _loc { get; set; } = default!;
    
    /// <summary>
    /// Message displayed on component
    /// </summary>
    private string _message = string.Empty;

    /// <summary>
    /// Determines whether the component should be rendered or not.
    /// </summary>
    private bool _showMessage = false;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        NotificationService.NotificationComponent = this;
    }

    /// <summary>
    /// Causes component to render an given message to be shown
    /// </summary>
    /// <param name="message"></param>
    public async Task Notify(string message)
    {
        _message = message;
        _showMessage = true;

        await InvokeAsync(StateHasChanged);
        
        await Task.Delay(2000);
        
        _showMessage = false;
        
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}