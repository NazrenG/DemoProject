﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskFlow.Entities.Data;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.Entities.Models;
using DemoProject.DTOs;
using TaskFlow.DataAccess.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly TaskFlowContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IQuizService _quizService;

        public static string CurrentUser;
        public AuthController(TaskFlowContext context, IConfiguration configuration, IUserService userService, IQuizService quizService)
        {
            _context = context;
            _configuration = configuration;
            _userService = userService;
            _quizService = quizService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegister userDto)
        {
            var ad = userDto.Username;
            var newUser = new User
            {
                UserName = userDto.Username,
                Email = userDto.Email,

                // AgeGroup = userDto.AgeGroup,
            };
            await _userService.Register(newUser, userDto.Password);
            await _quizService.Add(new Quiz());
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
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            if (user == null) return Unauthorized("Invalid username or password.");
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            CurrentUser = user.Id;
            return Ok(new
            {
                Token = tokenString,
                User = new
                {
                    Id = user.Id,
                    Username = user.UserName, 

                }
            });

        }

        [HttpGet("FindCurrentUserForToken")]
        public async Task<IActionResult> CurrentUserForToken(string token)
        {
            var user = _userService.GetUserByToken(token);

            if (user != null)
            {
                return Ok(new
                {
                    Username = user.Result.UserName,
                });
            }
            else
            {
                return BadRequest(new { });
            }
        }

        [HttpGet("RouteToQuiz")]
        public async Task<IActionResult> CheckQuizForm()
        {
            var item = await _quizService.GetQuizByUserId(CurrentUser);

            if (item == null)
            {
                return Ok(new
                {
                    Check = true,//quiz sehifesi acilsin
                });
            }
            return Ok(new
            {
                Check = false,//ana sehifeye kecsin
            });
        }


        [Authorize]
        [HttpGet("currentUser")]
        public IActionResult GetCurrentUser()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            return Ok(new
            {
                UserId = userId,
                Username = HttpContext.User.FindFirstValue(ClaimTypes.Name),
            });
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
                    Username = p.UserName,
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

        [HttpGet("UsersCount")]
        public async Task<IActionResult> GetUserCount()
        {
            var count = await _userService.GetAllUserCount();

            return Ok(count);
        }

        [HttpGet("PendingProjectCount")]
        public async Task<IActionResult> GetPendingProjectCount()
        {

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            var item = await _userService.GetUsers();
            var list = item.Where(p => p.Id == userId)
          .SelectMany(p => p.Projects ?? Enumerable.Empty<Project>())
          .Count(i => i.IsCompleted == false); ;
            return Ok(list);
        }
        [HttpGet("FinishedProjectCount")]
        public async Task<IActionResult> GetFinishedProjectCount(string token)
        {

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

             var item = await _userService.GetUsers();
            var list = item.Where(p => p.Id == userId)
          .SelectMany(p => p.Projects ?? Enumerable.Empty<Project>())
          .Count(i => i.IsCompleted == true);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<UserForRegister> GetUser(string id)
        {
            var item = await _userService.GetUserById(id);

            return new UserForRegister
            {
                Firstname = item.Firstname,
                Lastname = item.Lastname,
                Username = item.UserName,
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

        [HttpDelete("{token}")]
        public async Task<IActionResult> DeleteUser(string token)
        {
            var user = _userService.GetUserByToken(token);
            var item = await _userService.GetUserById(user.Result.Id);
            if (item != null)
            {
                await _userService.Delete(item);
                return Ok(item);
            }
            return NotFound(item);

        }

        [HttpPut("{token}")]
        public async Task<IActionResult> Put(string token, [FromBody] UserForRegister value)
        {
            var user = _userService.GetUserByToken(token);
            var item = await _userService.GetUserById(user.Result.Id);
            if (item != null)
            {
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
        [HttpPut("NameOrLastname/{token}")]
        public async Task<IActionResult> PutNameAndSurname(string token, [FromBody] UserForRegister value)
        {
            var user = _userService.GetUserByToken(token);
            var item = await _userService.GetUserById(user.Result.Id);
            if (item != null)
            {
                item.Firstname = value.Firstname;
                item.Lastname = value.Lastname;
                await _userService.Update(item);
                return Ok();
            }
            return BadRequest();

        }

    }

}