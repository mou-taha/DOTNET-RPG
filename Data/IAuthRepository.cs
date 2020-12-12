using System.Threading.Tasks;
using DOTNET_RPG.Models;
using DOTNET_RPG.Sevices;

namespace DOTNET_RPG.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user,string password);
        Task<ServiceResponse<string>> Login(string username,string password);
        Task<bool> UserExist(string username);
    }
}