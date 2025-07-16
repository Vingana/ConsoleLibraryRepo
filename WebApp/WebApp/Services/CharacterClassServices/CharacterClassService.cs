using WebApp.DTO.CharacterClass;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Services.CharacterClassServices;

public class CharacterClassService : ICharacterClassService
{
    private readonly ApplicationContext _context;

    public CharacterClassService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<CharacterClassDto>> GetAllAsync()
    {
        return await _context.CharacterClasses
            .Select(c => new CharacterClassDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            })
            .ToListAsync();
    }

    public async Task<CharacterClassDto?> GetByIdAsync(int id)
    {
        var cc = await _context.CharacterClasses.FindAsync(id);
        return cc == null ? null : new CharacterClassDto
        {
            Id = cc.Id,
            Name = cc.Name,
            Description = cc.Description
        };
    }

    public async Task<CharacterClassDto> CreateAsync(CharacterClassCreateDto dto)
    {
        var cc = new Models.CharacterClass
        {
            Name = dto.Name,
            Description = dto.Description
        };

        _context.CharacterClasses.Add(cc);
        await _context.SaveChangesAsync();

        return new CharacterClassDto
        {
            Id = cc.Id,
            Name = cc.Name,
            Description = cc.Description
        };
    }

    public async Task<bool> UpdateAsync(int id, CharacterClassUpdateDto dto)
    {
        var cc = await _context.CharacterClasses.FindAsync(id);
        if (cc == null) return false;

        cc.Name = dto.Name;
        cc.Description = dto.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cc = await _context.CharacterClasses.FindAsync(id);
        if (cc == null) return false;

        _context.CharacterClasses.Remove(cc);
        await _context.SaveChangesAsync();
        return true;
    }
}