using System.Linq;
using AutoMapper;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Dtos.Skill;
using DOTNET_RPG.Dtos.Fight;
using DOTNET_RPG.Dtos.Weapon;
using DOTNET_RPG.Models;

namespace DOTNET_RPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<Character, GetCharacterDto>().ForMember(dto => dto.Skills, 
            c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));
            CreateMap<Character, HightScoreDto>();
        }
    }
}