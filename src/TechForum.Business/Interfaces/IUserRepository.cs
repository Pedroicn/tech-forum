using TechForum.Business.Models;

namespace TechForum.Business.Interfaces;

public interface IUserRepository
{
  Task Add(User user);
  // Task Upadate(User user);
  // Task Remove(Guid id);
  Task<User> Login(string email, string password);
  Task<User> GetUser(Guid id);
  Task<List<User>> GetAll();

  Task AddTopics(User user, string title, string description);
}
