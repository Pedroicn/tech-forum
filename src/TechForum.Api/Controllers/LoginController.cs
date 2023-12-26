using Microsoft.AspNetCore.Mvc;
using TechForum.Business.Models;
using TechForum.Data.Context;

namespace TechForum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
  private AppDbContext _dbContext;
  // private string _email;
  // private string _password;
  public LoginController(AppDbContext dbContext)
  {
    _dbContext = dbContext;
    // _email = email;
    // _password = password;
  }

  [HttpPost]
  public ActionResult Login(string email, string password)
  {
    var user = _dbContext.Users.FirstOrDefault((user) => user.Email == email && user.Password == password);
    if (user == null)
    {
      return NotFound();
    }
    return Ok(user);
    
  }
}
