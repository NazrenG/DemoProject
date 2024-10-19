
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
        public async Task<IActionResult> Register([FromBody] UserForRegister userDto)
        {
            var ad = userDto.Username;
            var newUser = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,

                // AgeGroup = userDto.AgeGroup,
            };
              await _userService.Register(newUser, userDto.Password);
            return Ok(new
            {
                message = "User registered successfully",
                
            });
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLogin userDto)
        {

            var user = await _userService.Login(userDto.Username, userDto.Password);
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
        [HttpGet("AllUsers")]
        public async Task<IEnumerable<UserForRegister>> AllUsers()
        {
            var items = await _userService.GetUsers();
            var list = items.Select(p =>
            {
                return new UserForRegister
                {
                    Firstname = p.Firstname,
                    Lastname = p.Lastname,
                    Username = p.Username,
                    Email = p.Email,
                    Phone = p.Phone,
                    Occupation = p.Occupation,
                    Country = p.Country,
                    City = p.City,
                    Image = p.Image,
                    Gender = p.Gender,
                    IsOnline = p.IsOnline,
                };
            });
            return list;
        }
        [HttpGet("{id}")]
        public async Task<UserForRegister> GetUser(int id)
        {
            var item = await _userService.GetUserById(id);
            return new UserForRegister
            {
                Firstname = item.Firstname,
                Lastname = item.Lastname,
                Username = item.Username,
                Email = item.Email,
                Phone = item.Phone,
                Occupation = item.Occupation,
                Country = item.Country,
                City = item.City,
                Image = item.Image,
                Gender = item.Gender,
                IsOnline = item.IsOnline,
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var item = await _userService.GetUserById(id);
            if (item != null)
            {
                await _userService.Delete(item);
                return Ok(item);
            }
            return NotFound(item);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserForRegister value)
        {
            var item = await _userService.GetUserById(id);
            if (item != null)
            {
                item.Id = id;
                item.Username = value.Username;
                item.Firstname = value.Firstname;
                item.Lastname = value.Lastname;
                item.Email = value.Email;
                item.Phone = value.Phone;
                item.Occupation = value.Occupation;
                item.Country = value.Country;
                item.City = value.City;
                item.Image = value.Image;
                item.Gender = value.Gender;
                item.IsOnline = value.IsOnline;
                await _userService.Update(item);
                return Ok();
            }
            return BadRequest();

        }

    }

}
