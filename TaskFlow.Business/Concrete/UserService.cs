//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.Entities.Models;

namespace TaskFlow.DataAccess.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal dal;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(IUserDal dal, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this.dal = dal;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task Add(User user)
        {
            await dal.Add(user);
        }

        public async Task Delete(User user)
        {
            await dal.Delete(user);
        }

        public async Task<User> GetUserById(string id)
        {
            return await dal.GetById(f => f.Id == id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await dal.GetAll();
        }

        public async Task<User> Login(string username, string password)
        {
           // var user = await dal.GetById(f => f.UserName == username);
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) { return null; }
            var result=await _signInManager.CheckPasswordSignInAsync(user, password,false);
           
            return user;
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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task Update(User user)
        {
            await dal.Update(user);
        }

        public async Task<bool> UserExists(string username)
        {
            var hasExist = await dal.GetById(f => f.UserName == username);
            return hasExist != null ? true : false;
        }

        public async Task<int> GetAllUserCount()
        {
            var list = await dal.GetAll();
            return list.Count;
        }


        public Task<User> GetUserByToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var userIdClaim = jwtToken.Claims.First(claim => claim.Type == "nameid").Value;

                //int userId = int.Parse(userIdClaim);

                var user = dal.GetById(p => p.Id == userIdClaim);

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error decoding token: {ex.Message}");
                return null;
            }
        }
    }
}