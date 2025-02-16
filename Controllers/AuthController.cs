using Microsoft.AspNetCore.Mvc;
using AleniaAPI.Models.Auth;
using AleniaAPI.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AleniaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AleniaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AleniaContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AleniaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterModel model)
        {
            if (!new[] { "admin", "etablissement", "interimaire" }.Contains(model.Role.ToLower()))
            {
                return BadRequest("Rôle invalide");
            }

            if (await _context.Utilisateurs.AnyAsync(u => u.Email == model.Email))
            {
                return BadRequest("Email déjà utilisé");
            }

            var utilisateur = new Utilisateur
            {
                Email = model.Email,
                MotDePassHash = BCrypt.Net.BCrypt.HashPassword(model.MotDePass),
                Role = model.Role,
                DateCreation = DateTime.Now
            };

            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(utilisateur);

            return Ok(new AuthResponse
            {
                Token = token,
                Email = utilisateur.Email,
                Role = utilisateur.Role
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginModel model)
        {
            var user = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.MotDePass, user.MotDePassHash))
            {
                return Unauthorized("Email ou mot de passe incorrect");
            }

            var token = GenerateJwtToken(user);

            return Ok(new AuthResponse
            {
                Token = token,
                Email = user.Email,
                Role = user.Role
            });
        }

        private string GenerateJwtToken(Utilisateur user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}