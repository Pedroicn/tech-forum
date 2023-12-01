namespace TechForum.Business.Models;

public class Comment
{
  public Guid UserId { get; private set; }
  public string Description { get; private set; }
  
  public Comment(User user, string description)
  {
    UserId = user.Id;
    Description = description;
  }
}
