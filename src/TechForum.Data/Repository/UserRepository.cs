using TechForum.Business.Interfaces;
using TechForum.Business.Models;
using TechForum.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace TechForum.Data.Repository;

public class UserRepository : IUserRepository
{
  protected readonly AppDbContext Db;

  protected readonly DbSet<User> DbSet;
  protected readonly DbSet<Topic> DbTopic;
  protected readonly DbSet<Comment> DbComment;

  public UserRepository(AppDbContext db)
  {
    Db = db;
    DbSet = db.Set<User>();
    DbTopic = db.Set<Topic>();
    DbComment = db.Set<Comment>();

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
    return await DbSet.ToListAsync();
  }

  public async Task<User> GetUser(Guid id)
  {
    return await DbSet.FindAsync(id);
  }
  
  public async Task<User> GetUserByEmail(string email)
  {
    return Db.Users.FirstOrDefault((user) => user.Email == email);
  }

  public async Task RemoveUser(User user)
  {
    var topics = DbTopic.Where(t => t.UserId == user.Id).Include(c => c.Comments);
    
    DbTopic.RemoveRange(topics);
    Db.Users.Remove(user);
    await SaveChanges();
  }

  public async Task UpdateUser(User user)
  {
    DbSet.Update(user);
    await SaveChanges();
  }
  
}
