using System;
using System.Threading.Tasks;
using DOTNET_RPG.Data;
using DOTNET_RPG.Dtos;
using DOTNET_RPG.Dtos.Character.User;
using DOTNET_RPG.Models;
using DOTNET_RPG.Sevices;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_RPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo) =>
             _authRepo = authRepo;

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            ServiceResponse<int> reponse = await _authRepo.Register(
                new User { Username = request.Username }, request.Password
           );
            if (!reponse.success)
                return BadRequest(Response);
            return Ok(Response);
        }
    }
}