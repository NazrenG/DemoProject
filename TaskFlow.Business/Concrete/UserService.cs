using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.Entities.Models;

namespace TaskFlow.DataAccess.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _dal;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(IUserDal dal, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _dal = dal;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task Add(User user)
        {
            await _dal.Add(user);
        }

        public async Task Delete(User user)
        {
            await _dal.Delete(user);
        }

        public async Task<User> GetUserById(string id)
        {
            return await _dal.GetById(f => f.Id == id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dal.GetAll();
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded ? user : null;
        }

        public async Task<User> Register(User user, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists with this email.");
            }

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user.");
            }

            return user;
        }

        public async Task Update(User user)
        {
            await _dal.Update(user);
        }

        public async Task<bool> UserExists(string username)
        {
            var hasExist = await _userManager.FindByNameAsync(username);
            return hasExist != null;
        }

        public async Task<int> GetAllUserCount()
        {
            var users = await _dal.GetAll();
            return users.Count;
        }

        public async Task<User> GetUserByToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var userIdClaim = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                return await _userManager.FindByIdAsync(userIdClaim);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error decoding token: {ex.Message}");
                return null;
            }
        }
    }
}
