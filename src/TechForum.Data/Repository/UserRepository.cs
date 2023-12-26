using TechForum.Business.Interfaces;
using TechForum.Business.Models;
using TechForum.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace TechForum.Data.Repository;

public class UserRepository : IUserRepository
{
  protected readonly AppDbContext Db;

  protected readonly DbSet<User> DbSet;

  public UserRepository(AppDbContext db)
  {
    Db = db;
    DbSet = db.Set<User>();

  }

  public async Task<int> SaveChanges()
  {
    return await Db.SaveChangesAsync();
  }
  public async Task Add(User user)
  {
    DbSet.Add(user);
    await SaveChanges();
  }

  public async Task<List<User>> GetAll()
  {
    return await DbSet.Include(c => c.topics).ToListAsync();
  }

  public async Task<User> GetUser(Guid id)
  {
    return await DbSet.FindAsync(id);
  }
  
  public async Task<User> Login(string email, string password)
  {
    return Db.Users.FirstOrDefault((user) => user.Email == email && user.Password == password);
  }
  
  public async Task AddTopics(User user, string title, string description)
  {

      Topic topic = new Topic(user.Id, title, description);
      user.AddTopics(topic);
      DbSet.Update(user);
      await SaveChanges();
      
  }
  
  public async Task AddComments(User user, Topic topic, string description)
  {
    Comment comment = new Comment(user.Id, topic.TopicId, description);
    topic.AddComments(comment);
    Db.Update(topic);
    await SaveChanges();
      
  }
}
