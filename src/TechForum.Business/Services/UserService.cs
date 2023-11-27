using TechForum.Business.Interfaces;
using TechForum.Business.Models;

namespace TechForum.Business.Services;

public class UserService : IUserRepository
{
  private readonly IUserRepository _userrepository;
  public UserService(IUserRepository userrepository)
  {
    _userrepository = userrepository;
  }
  public async Task Add(User user)
  {
    await _userrepository.Add(user);
  }
}
