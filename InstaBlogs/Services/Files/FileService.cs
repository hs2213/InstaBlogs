using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Components.Forms;

namespace InstaBlogs.Services.Files;

public class FileService : IFileService
{
    private readonly BlobServiceClient _blobServiceClient;

    private readonly BlobContainerClient _container;

    private const string BasePath = "InstaBlogs";
    public FileService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;

        _container = _blobServiceClient.GetBlobContainerClient("videos");
    }

    public async Task<string> UploadFile(IBrowserFile file)
    {
        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
        
        string path = Path.Combine(BasePath, fileName);
        
        BlobClient client = _container.GetBlobClient(path);

        _ = await client.UploadAsync(file.OpenReadStream(100000000), overwrite: true);

        return $"https://uniblog.blob.core.windows.net/videos/{path}";
    }
}