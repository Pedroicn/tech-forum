using TechForum.Business.Models;

namespace TechForum.Api.Services;

public interface ITokenService
{
    string CreateToken(User user);
}