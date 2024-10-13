using DemoProject.Data;
using DemoProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DemoProject.Repos
{
    public class AuthRepos
    {
        private readonly DemoDb db;

        public AuthRepos(DemoDb db)
        {
            this.db = db;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Username == username);
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
            var add = user.Username;
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
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

        public async Task<bool> UserExists(string username)
        {
            var hasExist = await db.Users.AnyAsync(c => c.Username == username);
            return hasExist;
        }
    }
}
