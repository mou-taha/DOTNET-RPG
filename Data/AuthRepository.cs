using System;
using System.Threading.Tasks;
using DOTNET_RPG.Models;
using DOTNET_RPG.Sevices;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _DbContext;
        public AuthRepository(DataContext dbcontext)
        {
            _DbContext = dbcontext;
        }
        public Task<ServiceResponse<string>> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await UserExist(user.Username))
            {
                response.success = false;
                response.message = "User already exists.";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _DbContext.Users.AddAsync(user);
            await _DbContext.SaveChangesAsync();
            response.DATA = user.Id;
            return response;
        }

        public async Task<bool> UserExist(string username)
        {
            if (await _DbContext.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
                return true;
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}