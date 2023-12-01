namespace TechForum.Business.Models;

public class Topic
{
  public Guid UserId { get; private set; }
  public Guid TopicId { get; private set; }
  public string Description { get; private set; }
  public string Title { get; private set; }

  public int CommentAmount { get; private set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  private List<Comment> _comments;

  public IReadOnlyCollection<Comment> Comments => _comments;

  public Topic(Guid userId, string title, string description)
  {
    _comments = new List<Comment>();
    UserId = userId;
    TopicId = Guid.NewGuid();
    Title = title;
    Description = description;
    CreatedAt = DateTime.Now;
    UpdatedAt = DateTime.Now;
  }
  public void AddComments(Comment comment)
  {
    CommentAmount++;
    _comments.Add(comment);
  }

}
