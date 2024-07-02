using Auth0.AspNetCore.Authentication;
using InstaBlogs.Components;
using InstaBlogs.DBContext;
using InstaBlogs.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration.GetSection("Auth0").GetValue<string>("Domain")!;
    options.ClientId = builder.Configuration.GetSection("Auth0").GetValue<string>("ClientId")!;
});

builder.Services.AddDbContext<InstaBlogsDBContext>(options => 
    options.UseSqlite("DataSource=InstaBlogs.db"));

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddValidators();

builder.Services.AddHttpClient("Auth0ManagementAPI", client =>
{
    client.BaseAddress =
        new Uri($"https://{builder.Configuration.GetSection("Auth0").GetValue<string>("Domain")}/api/v2/");
});

builder.Services.AddHttpClient("Auth0Token", client =>
{
    client.BaseAddress = 
        new Uri($"https://{builder.Configuration.GetSection("Auth0").GetValue<string>("Domain")}/oath/token");
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/Account/Login", async (HttpContext httpContext, string redirectUri = "/") =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
        .WithRedirectUri(redirectUri)
        .Build();

    await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});

app.MapGet("/Account/Logout", async (HttpContext httpContext, string redirectUri = "/") =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
        .WithRedirectUri(redirectUri)
        .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();