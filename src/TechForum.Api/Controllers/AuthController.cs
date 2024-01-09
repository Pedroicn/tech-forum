using Microsoft.AspNetCore.Mvc;
using TechForum.Business.Models;
using TechForum.Api.Validations;
using TechForum.Business.Interfaces;
using TechForum.Api.Services;

namespace TechForum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    

    public AuthController(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    [HttpPost("register")]

    public async Task<ActionResult<User>> Register(string name, string email, string password)
    {
        var newUser = new User(name, email, password);
        UserValidation validation = new UserValidation();
        var validator = validation.Validate(newUser);

        List<User> users = await _userRepository.GetAll();
        var isExistingUser = users.Exists((user) => user.Email == email);
    
        if (validator.IsValid && !isExistingUser)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            newUser.Password = passwordHash;
            newUser.Role = "user";
            await _userRepository.Add(newUser);
            return Ok();
        }
        else if (!validator.IsValid)
        {
            return BadRequest(validator.Errors);
        }
        return BadRequest("There is already a user with this email");
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login(string email, string password)
    {
        User user = await _userRepository.GetUserByEmail(email);
        if (user == null)
        {
            return NotFound("User not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return BadRequest("Wrong password.");
        }

        var token = _tokenService.CreateToken(user);
        return Ok(token);
    }

}