using TechForum.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using TechForum.Data.Context;
using TechForum.Business.Models;

namespace TechForum.Data.Repository;

public class CommentRepository : ICommentRepository
{
    protected readonly AppDbContext Db;
    protected readonly DbSet<Comment> DbSet;

    public CommentRepository(AppDbContext db)
    {
        Db = db;
        DbSet = db.Set<Comment>();
    }
    
    public async Task<Comment> GetComment(Guid id)
    {
        return await DbSet.FindAsync(id);
    }
    
    public async Task AddComment(User user, Topic topic, string description)
    {
        Comment comment = new Comment(user.Id, topic.TopicId, description);
        topic.AddComments(comment);
        Db.Update(topic);
        await Db.SaveChangesAsync();
      
    }
    
    public async Task<List<Comment>> GetAllComments()
    {
        return await DbSet.ToListAsync();
    }
}