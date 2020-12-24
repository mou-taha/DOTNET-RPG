using System.Threading.Tasks;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Dtos.Weapon;

namespace DOTNET_RPG.Sevices.WeaponService
{
    public interface IWeaponService
    {
        public Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}