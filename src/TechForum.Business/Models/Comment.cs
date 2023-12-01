namespace TechForum.Business.Models;

public class Comment
{
  private Guid UserId;
  public string Description { get; private set; }
  
  public Comment(User user, string description)
  {
    UserId = user.Id;
    Description = description;
  }
}
