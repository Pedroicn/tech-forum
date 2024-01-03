using Microsoft.EntityFrameworkCore;
using TechForum.Business.Interfaces;
using TechForum.Business.Models;

using TechForum.Data.Context;

namespace TechForum.Data.Repository;

public class TopicRepository : ITopicRepository
{
    protected readonly AppDbContext Db;
    protected readonly DbSet<Topic> DbSet;

    public TopicRepository(AppDbContext db)
    {
        Db = db;
        DbSet = db.Set<Topic>();
    }
    
    public async Task<Topic> GetTopic(Guid id)
    {
        return await DbSet.FindAsync(id);
    }
    
    public async Task<List<Topic>> GetAllTopics()
    {
        return await DbSet.Include(c => c.Comments).ToListAsync();
    }
    public async Task AddTopics(User user, string title, string description)
    {

        Topic topic = new Topic(user.Id, title, description);
        user.AddTopics(topic);
        Db.Users.Update(user);
        await Db.SaveChangesAsync();
      
    }
    
}