using Microsoft.AspNetCore.Components.Forms;

namespace InstaBlogs.Services.Files;

public interface IFileService
{
    /// <summary>
    /// Uploads a file to the storage account and returns a URL to the file.
    /// </summary>
    /// <returns></returns>
    Task<string> UploadFile(IBrowserFile file);
}