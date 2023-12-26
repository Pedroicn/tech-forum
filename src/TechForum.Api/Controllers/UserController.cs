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
  private readonly ITopicRepository _topicRepository;

  public UserController(IUserRepository userRepository, ITopicRepository topicRepository)
  {
    _userRepository = userRepository;
    _topicRepository = topicRepository;
  }

  [HttpPost]

  public async Task<ActionResult> Add(string name, string email, string password)
  {
    var newUser = new User(name, email, password);
    UserValidation validation = new UserValidation();
    var validator = validation.Validate(newUser);

    List<User> users = await _userRepository.GetAll();
    var isExistingUser = users.Exists((user) => user.Email == email);
    

    if (validator.IsValid && !isExistingUser)
    {
      await _userRepository.Add(newUser);
      return Ok();
    }
    else if (!validator.IsValid)
    {
      return BadRequest(validator.Errors);
    }
    return BadRequest("There is already a user with this email");
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
  
  [HttpPost("comment")]
  public async Task<ActionResult> AddComments(Guid userId, Guid topicId, string description)
  {
    User user = await _userRepository.GetUser(userId);
    Topic topic = await _topicRepository.GetTopic(topicId);
    if (topic == null)
    {
      return NotFound("Invalid");
    }
    await _userRepository.AddComments(user, topic, description);
    return Ok(topic);
    
  }
  
  [HttpPost("login")]
  public async Task<ActionResult> Login(string email, string password)
  {
    User user = await _userRepository.Login(email, password);
    if (user == null)
    {
      return NotFound("Invalid email or password");
    }
    return Ok("User logged succesfully");
    
  }
}
