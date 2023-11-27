namespace TechForum.Business.Models;

public class User
{
  private readonly Guid Id;
  public string? Name { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }
}
