using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DOTNET_RPG.Data;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Sevices
{
    public class CharacterService : ICharacterService
    { //
        private static List<Character> characters = new List<Character>{
        new Character(){Id=0},
        new Character(){Name="same",Id=1}};

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            await _context.characters.AddAsync(character);
            await _context.SaveChangesAsync();
            return new ServiceResponse<List<GetCharacterDto>>
            {
                DATA = (_context.characters.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c))).ToList(),
            };
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> getAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>>
            {
                DATA = GetUserRole().Equals("Admin")?   _context.characters.Include(c=>c.Weapon).Include(c=>c.CharacterSkills).ThenInclude(c=>c.Skill).ToList().Select(c => _mapper.Map<GetCharacterDto>(c)).ToList():
                 (_context.characters.Include(c => c.Weapon).Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill).
        Where(c => c.User.Id == GetUserId()).ToList().Select(c => _mapper.Map<GetCharacterDto>(c))).ToList()
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character dbCharacter = await _context.characters.Include(c => c.Weapon)
        .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill).FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            if (dbCharacter == null)
            {
                serviceResponse.success = false;
                serviceResponse.message = "Charater not found!";
            }
            serviceResponse.DATA = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.characters.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                if (character.User.Id == GetUserId())
                {
                    character.Name = updateCharacter.Name;
                    character.Intelligence = updateCharacter.Intelligence;
                    character.Class = updateCharacter.Class;
                    character.Defense = updateCharacter.Defense;
                    character.HitPointes = updateCharacter.HitPointes;
                    character.Strength = updateCharacter.Strength;
                    _context.Update(character);
                    _context.SaveChanges();
                    serviceResponse.DATA = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    serviceResponse.success = false;
                    serviceResponse.message = "Character not found.";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if (character != null)
                {
                    _context.characters.Remove(character);
                    await _context.SaveChangesAsync();
                    serviceResponse.DATA = (_context.characters.Where(c => c.User.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
                }
                else
                {
                    serviceResponse.success = false;
                    serviceResponse.message = "character not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = ex.Message;
            }
            return serviceResponse;
        }

        private int GetUserId() =>
        int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

 private string  GetUserRole() =>
        _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

    }
}