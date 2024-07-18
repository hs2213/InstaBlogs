using InstaBlogs.Components.SubComponents;
using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;
using InstaBlogs.Services.Blogs;
using InstaBlogs.Services.Files;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace InstaBlogs.Components.Pages;

public partial class CreateBlog : IAsyncDisposable
{
    [Parameter]
    public Guid Id { get; set; }
    
    private Blog _blogBeingEdited = new Blog();

    private string _typedContent = string.Empty;
    
    private string _convertedContent = string.Empty;
    
    private string _selectedText = string.Empty;

    private Loader _loader = default!;

    private IJSObjectReference _module = default!;
    
    [Inject]
    private IFileService FileService { get; set; } = default!;
    
    [Inject]
    private IJSRuntime JsRuntime { get; set; } = default!;
    
    [Inject]
    private IBlogService BlogService { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        if (Id == Guid.Empty)
        {
            return;
        }

        var retrievedBlog = await BlogService.GetById(Id);

        if (retrievedBlog == null)
        {
            return;
        }

        _blogBeingEdited = retrievedBlog;

        _typedContent = _blogBeingEdited.Content;
        UpdateContent();
        
        StateHasChanged();
    }

    private void UpdateContent()
    {
        _convertedContent = Markdig.Markdown.ToHtml(_typedContent);
    }
    
    private async Task UploadImage(InputFileChangeEventArgs args)
    {
        _loader.Show();
        
        string filePath = await FileService.UploadFile(args.File);
        
        _typedContent += $"![Image]({filePath})";
        UpdateContent();
        
        _loader.Hide();
    }
    
    private async Task UploadVideo(InputFileChangeEventArgs args)
    {
        _loader.Show();
        
        string filePath = await FileService.UploadFile(args.File);
        
        _typedContent += $"""<video controls><source src="{filePath}"/></video>""";
        UpdateContent();
        
        StateHasChanged();
        
        _loader.Hide();
    }

    private async Task OnMouseUp()
    {
        await LoadModule();
        
        _selectedText = await _module.InvokeAsync<string>("GetSelectedText");
    }
    
    private void ModifyText(ModificationType modificationType)
    {
        switch (modificationType)
        {
            case ModificationType.Bold:
                _typedContent = _typedContent.Replace(_selectedText, $"**{_selectedText.Trim()}** ");
                break;
            case ModificationType.Italic:
                _typedContent = _typedContent.Replace(_selectedText, $"*{_selectedText.Trim()}* ");
                break;
        }
        
        StateHasChanged();
    }

    private void AddLink()
    {
        _typedContent += $"[Enter Title Here](Enter URL Here)";
        
        StateHasChanged();
    }
    
    private async Task LoadModule()
    {
        if (_module != null) 
        {
            return;
        }
        
        _module = await JsRuntime
            .InvokeAsync<IJSObjectReference>("import", "./Components/Pages/CreateBlog.razor.js");
    }

    private async Task SaveBlog()
    {
        _blogBeingEdited.Content = _typedContent;

        if (Id == Guid.Empty)
        {
            await BlogService.Create(_blogBeingEdited);
        }

        await BlogService.Update(_blogBeingEdited);
    }

    public async ValueTask DisposeAsync()
    {
        if (_module != null)
        {
            await _module.DisposeAsync();
        }
        
        GC.SuppressFinalize(this);
    }
}