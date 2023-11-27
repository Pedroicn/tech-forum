using Microsoft.EntityFrameworkCore;
using TechForum.Business.Models;
namespace TechForum.Data.Context;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
  public AppDbContext()
  {

  }

  public DbSet<User> Users { get; set; }
}
