using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DOTNET_RPG.Data;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Dtos.Weapon;
using DOTNET_RPG.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Sevices.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _Context;
        private readonly IHttpContextAccessor _Httpcontextaccessor;
        private readonly IMapper _Mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpcontextaccessor, IMapper mapper)
        {
            _Mapper = mapper;
            _Httpcontextaccessor = httpcontextaccessor;
            _Context = context;
        }


        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                int id=int.Parse(_Httpcontextaccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                Character character = await _Context.characters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId &&
                  c.User.Id == id);
                if (character == null)
                {
                    response.success = false;
                    response.message = "character not found!";
                }
                Weapon weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    CharacterId = newWeapon.CharacterId
                };
                await _Context.Weapons.AddAsync(weapon);
                await _Context.SaveChangesAsync();
                response.DATA = _Mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }
    }
}