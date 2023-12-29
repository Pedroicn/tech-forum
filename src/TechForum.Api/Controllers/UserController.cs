using System.Net;
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
  
}
