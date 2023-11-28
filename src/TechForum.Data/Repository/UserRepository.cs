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
    return await DbSet.ToListAsync();
  }
}