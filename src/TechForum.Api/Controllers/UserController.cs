using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechForum.Api.Validations;
using TechForum.Business.Interfaces;
using TechForum.Business.Models;
namespace TechForum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserRepository _userRepository;

  public UserController(IUserRepository userRepository, ITopicRepository topicRepository)
  {
    _userRepository = userRepository;
  }

  [HttpGet]

  public async Task<ActionResult> GetAll()
  {
    try
    {
      List<User> users = await _userRepository.GetAll();
      return Ok(users);
    }
    catch (Exception error)
    {
      return BadRequest(new { message = error.Message.ToString() });

    }
  }

  [HttpGet("{id}")]

  public async Task<ActionResult> GetUser(Guid id)
  {
    try
    {
      User user = await _userRepository.GetUser(id);
      if (user == null)
      {
        return NotFound();
      }
      return Ok(user);
    }
    catch (Exception error)
    {
      return BadRequest(new { message = error.Message.ToString() });

    }
  }
  
  [HttpDelete("{id}")]
  public async Task<ActionResult> RemoveUser(Guid id)
  {
    try
    {
      User user = await _userRepository.GetUser(id);
      if (user == null)
      {
        return NotFound();
      }
      await _userRepository.RemoveUser(user);
      return NoContent();
    }
    catch (DbUpdateException error)
    {
      return BadRequest(new { message = error.InnerException.Message });

    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> UpdateUser(Guid id, string name, string? email)
  {
    User newUser = await _userRepository.GetUser(id);
    List<User> users = await _userRepository.GetAll();
    var isExistingUser = users.Exists((user) => user.Email == email);
    
    newUser.Name = name;
    newUser.Email = email;
    newUser.UpdatedAt = DateTime.Now;
    
    UserValidation validation = new UserValidation();
    var validator = validation.Validate(newUser);

    if (validator.IsValid && !isExistingUser)
    {
      await _userRepository.UpdateUser(newUser);
      return Ok();
    }
    else if (!validator.IsValid)
    {
      return BadRequest(validator.Errors);
    }

    return BadRequest("There is already a user with this email");
  }
}
