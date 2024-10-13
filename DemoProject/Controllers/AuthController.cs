 
using Microsoft.AspNetCore.Mvc; 
using DemoProject.Data;
using DemoProject.Entities;
using DemoProject.Models;
using DemoProject.Repos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthController : ControllerBase
    {
        private readonly DemoDb _context;
       private readonly IConfiguration _configuration;
        private readonly AuthRepos authRepos;

        public AuthController(DemoDb context, IConfiguration configuration,AuthRepos authRepos)
        {
            _context = context;
            this.authRepos = authRepos;
            _configuration = configuration;
        }
       

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserForRegiter userDto)
        {
            var ad = userDto.Username;
            var newUser = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                AgeGroup = userDto.AgeGroup,
            }; 
            await authRepos.Register(newUser, userDto.Password);
            return StatusCode(StatusCodes.Status201Created);

        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLgin userDto)
        {  

            var user=await authRepos.Login(userDto.Username, userDto.Password);
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(tokenString);

        }

    }

}
