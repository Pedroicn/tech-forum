namespace TechForum.Business.Models;
public class User
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  
  public string Role { get; set; }
  public int TopicAmount { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  private List<Topic> _topics;
  public IReadOnlyCollection<Topic> topics => _topics;

  public User(string name, string email, string password)
  {
    _topics = new List<Topic>();
    Id = Guid.NewGuid();
    Name = name;
    Email = email;
    Password = password;
    CreatedAt = DateTime.Now;
    UpdatedAt = DateTime.Now;
  }

  public void AddTopics(Topic topic)
  {
    TopicAmount++;
    _topics.Add(topic);
  }

  public string DeleteTopic(Topic topic)
  {
    if (topic.UserId == Id && !topic.Comments.Any())
    {
      TopicAmount--;
      _topics.Remove(topic);
      return "Topic removed";
    } 
    else if (topic.UserId != Id && !topic.Comments.Any())
    {
      return "You don't have authorization to remove this topic";
    }
    else
    {
      return "There is already comments in this topic";
    }

  }
  
  

}
