 
using Microsoft.AspNetCore.Mvc;    
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskFlow.Entities.Data;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.Entities.Models;
using DemoProject.DTOs;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthController : ControllerBase
    {
        private readonly TaskFlowContext _context;
       private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(TaskFlowContext context, IConfiguration configuration, IUserService userService)
        {
            _context = context;
            _configuration = configuration;
            _userService = userService;
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
            await _userService.Register(newUser, userDto.Password);
            return StatusCode(StatusCodes.Status201Created);

        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLgin userDto)
        {  

            var user=await _userService.Login(userDto.Username, userDto.Password);
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
