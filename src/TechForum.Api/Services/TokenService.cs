using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TechForum.Business.Interfaces;
using TechForum.Business.Models;

namespace TechForum.Api.Services;

public class TokenService: ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public TokenService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public string CreateToken(User user)
    {

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? String.Empty));
        var issuer = _configuration["jwt:Issuer"];

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            claims: new[]
            {
                new Claim(type: ClaimTypes.Email, user.Email),
                new Claim(type: ClaimTypes.Role, user.Role)
            },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return token;
    }
}