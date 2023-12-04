namespace TechForum.Business.Models;

public class Comment
{
  public Guid UserId { get; private set; }
  public Guid CommentId { get; private set; }
  public string Description { get; private set; }
  
  public Comment(Guid userId, string description)
  {
    UserId = userId;
    CommentId = Guid.NewGuid();
    Description = description;
  }
}
