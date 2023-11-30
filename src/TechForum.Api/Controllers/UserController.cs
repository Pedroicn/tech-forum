using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TechForum.Api.Validations;
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
      UserValidation validator = new UserValidation();
      validator.ValidateAndThrow(newUser);
      await _userRepository.Add(newUser);
      return Created("", newUser);
    }
    catch(ValidationException error)
    {
      return BadRequest(new { message = error.Message.ToString() });
    }
  }

  [HttpGet]

  public async Task<ActionResult> GetAll()
  {
    try
    {;
      List<User> users = await _userRepository.GetAll();
      return Ok(users);
    }
    catch(Exception error)
    {
      return BadRequest(new { message = error.Message.ToString() });

    }
  }
}
