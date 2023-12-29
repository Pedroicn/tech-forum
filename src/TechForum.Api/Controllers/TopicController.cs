using Microsoft.AspNetCore.Mvc;
using TechForum.Business.Interfaces;
using TechForum.Business.Models;

namespace TechForum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TopicController : ControllerBase
{
    private readonly ITopicRepository _topicRepository;
    private readonly IUserRepository _userRepository;

    public TopicController(ITopicRepository topicRepository, IUserRepository userRepository)
    {
        _topicRepository = topicRepository;
        _userRepository = userRepository;
    }
    
    [HttpGet("{id}")]

    public async Task<ActionResult> GetTopic(Guid id)
    {
        try
        {
            Topic topic = await _topicRepository.GetTopic(id);
            if (topic == null)
            {
                return NotFound();
            }
            return Ok(topic);
        }
        catch (Exception error)
        {
            return BadRequest(new { message = error.Message.ToString() });

        }
    }
    
    [HttpPost]
    public async Task<ActionResult> AddTopics(Guid userId, string title, string description)
    {
      User user = await _userRepository.GetUser(userId);
      if (user == null)
      {
        return NotFound("Invalid");
      }
      await _topicRepository.AddTopics(user, title, description);
      return Ok(user);
      
    }
    
    [HttpGet]

    public async Task<ActionResult> GetAllComments()
    {
        try
        {
            List<Topic> topic = await _topicRepository.GetAllTopics();
            return Ok(topic);
        }
        catch (Exception error)
        {
            return BadRequest(new { message = error.Message.ToString() });

        }
    }
}