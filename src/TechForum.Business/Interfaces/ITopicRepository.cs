using TechForum.Business.Models;

namespace TechForum.Business.Interfaces;

public interface ITopicRepository
{
    Task AddTopics(User user, string title, string description);
    Task<Topic> GetTopic(Guid id);

}