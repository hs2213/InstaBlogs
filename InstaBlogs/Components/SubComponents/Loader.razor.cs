namespace InstaBlogs.Components.SubComponents;

public partial class Loader
{
    private bool _active = false;
    
    public void Show()
    {
        _active = true;
        
        StateHasChanged();
    }
    
    public void Hide()
    {
        _active = false;
        StateHasChanged();
    }
}