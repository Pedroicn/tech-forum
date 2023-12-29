using Microsoft.AspNetCore.Mvc;
using TechForum.Business.Interfaces;
using TechForum.Business.Models;

namespace TechForum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CommentController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ITopicRepository _topicRepository;
    private readonly ICommentRepository _commentRepository;

    public CommentController(
        IUserRepository userRepository, 
        ITopicRepository topicRepository,
        ICommentRepository commentRepository
        )
    {
        _userRepository = userRepository;
        _topicRepository = topicRepository;
        _commentRepository = commentRepository;
    }
    
    [HttpPost]
    
    public async Task<ActionResult> AddComment(Guid userId, Guid topicId, string description)
    {
        User user = await _userRepository.GetUser(userId);
        Topic topic = await _topicRepository.GetTopic(topicId);
        if (topic == null)
        {
            return NotFound("Invalid");
        }
        await _commentRepository.AddComment(user, topic, description);
        return Ok(topic);
    
    }
    
    [HttpGet("{id}")]

    public async Task<ActionResult> GetComment(Guid id)
    {
        try
        {
            Comment comment = await _commentRepository.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }
        catch (Exception error)
        {
            return BadRequest(new { message = error.Message.ToString() });

        }
    }
    
    [HttpGet]

    public async Task<ActionResult> GetAllComments()
    {
        try
        {
            List<Comment> comment = await _commentRepository.GetAllComments();
            return Ok(comment);
        }
        catch (Exception error)
        {
            return BadRequest(new { message = error.Message.ToString() });

        }
    }
}