using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DOTNET_RPG.Data;
using DOTNET_RPG.Dtos.Fight;
using DOTNET_RPG.Models;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Sevices.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FightService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            ServiceResponse<FightResultDto> response = new ServiceResponse<FightResultDto>
            {
                DATA = new FightResultDto()
            };
            try
            {
                List<Character> characters = await _context.characters
                .Include(c => c.Weapon).Include(c => c.CharacterSkills)
                .ThenInclude(cs => cs.Skill).Where(c => request.CharacterIds.Contains(c.Id)).ToListAsync();
                bool defeated = false;
                while (!defeated)
                {
                    foreach (Character attacker in characters)
                    {
                        List<Character> opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        Character opponent = opponents[new Random().Next(opponents.Count)];
                        int damage = 0;
                        string attackUsed = string.Empty;
                        bool useweapon = new Random().Next(2) == 0;
                        if (useweapon)
                        {
                            attackUsed = attacker.Weapon.Name;
                            damage = DoWeaponAttack(attacker, opponent);
                        }
                        else
                        {
                            int randomSkill = new Random().Next(attacker.CharacterSkills.Count);
                            attackUsed = attacker.CharacterSkills[randomSkill].Skill.Name;
                            damage = DoSkillAttack(attacker, opponent, attacker.CharacterSkills[randomSkill]);
                        }
                        response.DATA.Log.Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage.");
                        if (opponent.HitPointes <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            response.DATA.Log.Add($"{opponent.Name} has been defeated!");
                            response.DATA.Log.Add($"{attacker.Name} wins with {attacker.HitPointes} HP left!");
                            break;
                        }
                    }
                }
                characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPointes = 100;
                });
                _context.characters.UpdateRange(characters);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<AttackResultatDto>> WeaponAttack(WeaponAttackDto request)
        {
            ServiceResponse<AttackResultatDto> response = new ServiceResponse<AttackResultatDto>();
            try
            {
                Character attacker = await _context.characters
                .Include(c => c.Weapon)
                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                Character opponent = await _context.characters
                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);
                int damage = DoWeaponAttack(attacker, opponent);
                if (opponent.HitPointes <= 0)
                    response.message = $"{opponent.Name} has been defeated";
                _context.characters.Update(opponent);
                await _context.SaveChangesAsync();
                response.DATA = new AttackResultatDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPointes,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPointes,
                    Damage = damage
                };
            }
            catch (System.Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        private static int DoWeaponAttack(Character attacker, Character opponent)
        {
            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            damage -= new Random().Next(opponent.Defense);
            if (damage > 0)
                opponent.HitPointes -= (int)damage;
            return damage;
        }

        private static int DoSkillAttack(Character attacker, Character opponent, DOTNET_RPG.Models.CharacterSkill characterSkill)
        {
            int damage = characterSkill.Skill.Damage + (new Random().Next(attacker.Intelligence));
            damage -= new Random().Next(opponent.Defense);
            if (damage > 0)
                opponent.HitPointes -= (int)damage;
            return damage;
        }

        public async Task<ServiceResponse<List<HightScoreDto>>> GetHighscore()
        {
            List<Character> characters = await _context.characters
            .Where(c => c.Fights > 0)
            .OrderByDescending(c => c.Victories)
            .ThenBy(c => c.Defeats)
            .ToListAsync();
            var response = new ServiceResponse<List<HightScoreDto>>
            {
                DATA = characters.Select(c => _mapper.Map<HightScoreDto>(c)).ToList()
            };
            return response;
        }
    }
}