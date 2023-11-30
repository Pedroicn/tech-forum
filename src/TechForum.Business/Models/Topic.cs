namespace TechForum.Business.Models;

public class Topic
{
  public Guid UserId { get; private set; }

  public Guid TopicId { get; private set; }
  public string Description { get; private set; }
  public string Title { get; private set; }

  public int CommentAmount { get; private set; }

  private List<Comment> _comments;

  public IReadOnlyCollection<Comment> comments => _comments;

  public void AddComments(Comment comment)
  {
    CommentAmount++;
    _comments.Add(comment);
  }

}
