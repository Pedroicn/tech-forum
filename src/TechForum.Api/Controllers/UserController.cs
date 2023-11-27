using Microsoft.AspNetCore.Mvc;
using TechForum.Business.Interfaces;
using TechForum.Business.Models;
namespace TechForum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserRepository? _userRepository;

  public UserController(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  [HttpPost]

  public async Task<ActionResult> Add(string name, string email, string password)
  {
    try
    {
      var newUser = new User(name, email, password);
      await _userRepository.Add(newUser);
      return Created("", newUser);
    }
    catch(Exception error)
    {
      return BadRequest(new { message = error.Message.ToString() });

    }
  }
}
