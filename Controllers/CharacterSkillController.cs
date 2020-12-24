using System.Threading.Tasks;
using DOTNET_RPG.Dtos.CharacterSkill;
using DOTNET_RPG.Sevices.CharacterSkill;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_RPG.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CharacterSkillController : ControllerBase
    {
        private readonly ICharacterSkillService _characterSkillService;
        public CharacterSkillController(ICharacterSkillService characterSkillService)
        {
            _characterSkillService = characterSkillService;

        }

        [HttpPost("Add")]
        public async Task<IActionResult> addcharacterskill(AddCharacterSkillDto newCharacterSkill)
        {
         return Ok(await _characterSkillService.addCharacterSkill(newCharacterSkill));   
        }
    }
}