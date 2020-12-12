using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DOTNET_RPG.Data;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Models;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Sevices
{
    public class CharacterService : ICharacterService
    {
        //test
        private static List<Character> characters = new List<Character>{
        new Character(){Id=0},
        new Character(){Name="same",Id=1}};

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            _context.characters.Add(_mapper.Map<Character>(newCharacter));
            _context.SaveChanges();
            return new ServiceResponse<List<GetCharacterDto>>
            {
                DATA = (_context.characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList(),
            };
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> getAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>>
            {
                DATA = (_context.characters.ToList().Select(c => _mapper.Map<GetCharacterDto>(c))).ToList()
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            return new ServiceResponse<GetCharacterDto>
            {
                DATA = _mapper.Map<GetCharacterDto>(_context.characters.Find(id)),
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = _context.characters.Find(updateCharacter.Id);
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
                Character character = _context.characters.First(c => c.Id == id);
                _context.characters.Remove(character);
                _context.SaveChanges();
                serviceResponse.DATA = (_context.characters.ToList().Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = ex.Message;
            }
            return serviceResponse;
        }
    }
}