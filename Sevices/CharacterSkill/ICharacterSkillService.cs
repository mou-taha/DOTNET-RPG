using System.Threading.Tasks;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Dtos.CharacterSkill;

namespace DOTNET_RPG.Sevices.CharacterSkill
{
    public interface ICharacterSkillService
    {
        public Task<ServiceResponse<GetCharacterDto>> addCharacterSkill(AddCharacterSkillDto newCharacterSkill);
        
    }
}