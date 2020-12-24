using System.Collections.Generic;
using System.Threading.Tasks;
using DOTNET_RPG.Dtos.Fight;

namespace DOTNET_RPG.Sevices.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultatDto>> WeaponAttack(WeaponAttackDto request);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);
        Task<ServiceResponse<List<HightScoreDto>>> GetHighscore();

    }
}