using System.Threading.Tasks;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Dtos.CharacterSkill;
using DOTNET_RPG.Data;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DOTNET_RPG.Models;
using System;

namespace DOTNET_RPG.Sevices.CharacterSkill
{
    public class CharacterSkillService : ICharacterSkillService
    {

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _Httpcontextaccessor;
        private readonly IMapper _Mapper;

        public CharacterSkillService(DataContext context, IHttpContextAccessor httpcontextaccessor, IMapper mapper)
        {
            _context = context;
            _Httpcontextaccessor = httpcontextaccessor;
            _Mapper = mapper;
        }
        public async Task<ServiceResponse<GetCharacterDto>> addCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.characters
                .Include(c => c.Weapon)
                .Include(c => c.CharacterSkills)
                .ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId &&
                        c.User.Id == int.Parse(_Httpcontextaccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if (character == null)
                {
                    response.success = false;
                    response.message = "character not found!";
                    return response;
                }
                Skill skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if (skill == null)
                {
                    response.success = false;
                    response.message = "skill not found";
                    return response;
                }
                DOTNET_RPG.Models.CharacterSkill characterSkill = new DOTNET_RPG.Models.CharacterSkill
                {
                    Character = character,
                    Skill = skill
                };
                await _context.characterSkills.AddAsync(characterSkill);
                await _context.SaveChangesAsync();
                response.DATA = _Mapper.Map<GetCharacterDto>(character);
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
                return response;
            }
        }
    }
}