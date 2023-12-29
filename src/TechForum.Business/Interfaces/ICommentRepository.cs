using TechForum.Business.Models;

namespace TechForum.Business.Interfaces;

public interface ICommentRepository
{
    Task AddComment(User user, Topic topic, string description);
    Task<Comment> GetComment(Guid id);
    Task<List<Comment>> GetAllComments();
}