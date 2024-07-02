using InstaBlogs.Entities;

namespace InstaBlogs.Services.Auth0;

public interface IAuth0Service
{
     /// <summary>
     /// Assigns a auth0 role to a user - used for determining if a user can access certain pages.
     /// </summary>
     /// <param name="user"></param>
     /// <returns></returns>
     Task AssignRole(User user);
}