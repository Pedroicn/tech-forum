using TechForum.Business.Models;

namespace TechForum.Business.Interfaces;

public interface IUserRepository
{
  Task Add(User user);
  Task UpdateUser(User user);
  Task RemoveUser(User user);
  Task<User> Login(string email);
  Task<User> GetUser(Guid id);
  Task<List<User>> GetAll();

}
