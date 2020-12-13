using System;
using System.Threading.Tasks;
using DOTNET_RPG.Models;
using DOTNET_RPG.Sevices;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DOTNET_RPG.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _DbContext;

        public AuthRepository(DataContext dbcontext)
        {
            _DbContext = dbcontext;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                response.DATA = null;
                response.message = "user not found.";
                response.success = false;
            }
            else if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                response.success = false;
                response.message = "Wrong password!";
            }
            else
                response.DATA = CreateToken(user);

            return response;
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
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != passwordHash[i])
                        return false;
                return true;
            }
        }

        private string CreateToken(User user)
        {
            return string.Empty;
        }
    }
}