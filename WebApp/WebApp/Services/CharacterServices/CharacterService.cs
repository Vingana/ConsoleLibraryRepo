using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.DTO.Character;

namespace WebApp.Services.CharacterServices;

public class CharacterService : ICharacterService
{
    private readonly ApplicationContext _context;

    public CharacterService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<CharacterDto?> GetCharacterByIdAsync(int id, int userId)
    {
        var character = await _context.Characters
        .Include(c => c.CharacterClass)
        .Include(c => c.Race)
        .Include(c => c.Faction)
        .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);


        if (character == null) return null;

        return new CharacterDto
        {
            Id = character.Id,
            Name = character.Name,
            CharacterClass = character.CharacterClass.Name,
            Race = character.Race.Name,
            Faction = character.Faction.Name,
            Description = character.Description
        };
    }

    public async Task<IEnumerable<CharacterDto>> GetAllCharactersAsync(int userId)
    {
        return await _context.Characters
            .Include(c => c.CharacterClass)
            .Include(c => c.Race)
            .Include(c => c.Faction)
            .Where(c => c.UserId == userId)
            .Select(c => new CharacterDto
            {
                Id = c.Id,
                Name = c.Name,
                CharacterClass = c.CharacterClass.Name,
                Race = c.Race.Name,
                Faction = c.Faction.Name,
                Description = c.Description
            })
            .ToListAsync();
    }

    public async Task<int> CreateCharacterAsync(CharacterCreateDto dto, int userId)
    {
        var character = new Models.Character
        {
            Name = dto.Name,
            UserId = userId,
            CharacterClassId = dto.CharacterClassId,
            RaceId = dto.RaceId,
            FactionId = dto.FactionId,
            Description = dto.Description
        };

        _context.Characters.Add(character);
        await _context.SaveChangesAsync();

        return character.Id;
    }

    public async Task<bool> UpdateCharacterAsync(int id, CharacterUpdateDto dto, int userId)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        if (character == null) return false;

        character.Name = dto.Name;
        character.CharacterClassId = dto.CharacterClassId;
        character.RaceId = dto.RaceId;
        character.FactionId = dto.FactionId;
        character.Description = dto.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCharacterAsync(int id, int userId)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        if (character == null) return false;

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync();

        return true;
    }
}
