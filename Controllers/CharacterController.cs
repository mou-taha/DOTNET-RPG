using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Models;
using DOTNET_RPG.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_RPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        
        private readonly ICharacterService _CharacterService;

        public CharacterController(ICharacterService characterservice)
        {
            _CharacterService = characterservice;
        }

       // [AllowAnonymous]
        [HttpGet("getAll")]
        public async Task<IActionResult> Get()
        {
            int id=int.Parse(User.Claims.FirstOrDefault(u=>u.Type==ClaimTypes.NameIdentifier).Value);
            return Ok(await _CharacterService.getAllCharacters(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _CharacterService.GetCharacterById(id));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCharacter(AddCharacterDto c)
        {
            return Ok(await _CharacterService.AddCharacter(c));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = await _CharacterService.UpdateCharacter(updateCharacter);
            if (serviceResponse.DATA == null)
                return NotFound(Response);
            return Ok(serviceResponse);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = await _CharacterService.DeleteCharacter(id);
            if (serviceResponse.DATA == null)
                return NotFound(Response);
            return Ok(serviceResponse);
        }
    }
}