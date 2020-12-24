using System.Threading.Tasks;
using DOTNET_RPG.Dtos.Weapon;
using DOTNET_RPG.Sevices.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_RPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _WeaponService ;

        public WeaponController(IWeaponService weaponService)
        {
            _WeaponService = weaponService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddWeapon(AddWeaponDto newWeapon){
            return Ok(await _WeaponService.AddWeapon(newWeapon));
        } 

    }
}