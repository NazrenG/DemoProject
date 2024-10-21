using System;
using System.Collections.Generic;
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

        public UserService(IUserDal dal)
        {
            this.dal = dal;
        }

        public async Task Add(User user)
        {
            await dal.Add(user);
        }

        public async Task Delete(User user)
        {
            await dal.Delete(user);
        }

        public async Task<User> GetUserById(int id)
        {
            return await dal.GetById(f => f.Id == id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await dal.GetAll();
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await dal.GetById(f => f.Username == username);
            if (user == null) { return null; }
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[]? passwordHash, byte[]? passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash); ;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            var list = await dal.GetAll(u => u.Email == user.Email);
            if (list.Count == 0)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;
                await dal.Add(user);
                return user;
            }
            throw new Exception("User already exists with this email.");


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
            var hasExist = await dal.GetById(f => f.Username == username);
            return hasExist != null ? true : false;
        }

        public async Task<int> GetAllUserCount()
        {
            var list = await dal.GetAll();
            return list.Count;
        }
    }
}
