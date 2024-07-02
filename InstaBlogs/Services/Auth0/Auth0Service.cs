using System.Text;
using System.Text.Json;
using InstaBlogs.Entities;
using InstaBlogs.Entities.Enums;

namespace InstaBlogs.Services.Auth0;

public class Auth0Service
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    
    public Auth0Service(
        IHttpClientFactory httpClientFactory, 
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task AssignRole(User user)
    {
        string token = await GetToken(user);

        if (string.IsNullOrWhiteSpace(token))
        {
            return;
        }
        
        HttpClient client = _httpClientFactory.CreateClient("Auth0Management");
        
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"users/{user.Id}/roles")
        {
            Content = new StringContent(JsonSerializer.Serialize(new
            {
                roles = new[] { GetRoleId(user.Role) }
            }), Encoding.UTF8, "application/json")
        };
        
        HttpResponseMessage response = await client.SendAsync(request);
        
        response.EnsureSuccessStatusCode();
    }

    public async Task<string> GetToken(User user)
    {
        HttpClient client = _httpClientFactory.CreateClient("Auth0Token");
        
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, string.Empty)
        {
            Content = new StringContent(JsonSerializer.Serialize(new
            {
                client_id = user.Id,
                client_secret = _configuration["Auth0:ClientSecret"],
                grant_type = "client_credentials"
            }), Encoding.UTF8, "application/json")
        };
        
        HttpResponseMessage response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        TokenResponse? responseContent = await response.Content.ReadFromJsonAsync<TokenResponse>();

        return responseContent == null 
            ? string.Empty 
            : responseContent.AccessToken;
    }

    private string GetRoleId(Role role)
    {
        return role switch
        {
            Role.Admin => _configuration["Auth0:Roles:Admin"]!,
            Role.Creator => _configuration["Auth0:Roles:Creator"]!,
            Role.Viewer => _configuration["Auth0:Roles:Viewer"]!,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }

}