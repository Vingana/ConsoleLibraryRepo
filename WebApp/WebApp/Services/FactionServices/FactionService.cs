using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.DTO.Faction;
using WebApp.Models;

namespace WebApp.Services.FactionServices;

public class FactionService : IFactionService
{
    private readonly ApplicationContext _context;

    public FactionService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<FactionDto>> GetAllAsync()
    {
        return await _context.Factions
            .Select(f => new FactionDto
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description
            })
            .ToListAsync();
    }

    public async Task<FactionDto?> GetByIdAsync(int id)
    {
        var faction = await _context.Factions.FindAsync(id);
        return faction == null ? null : new FactionDto
        {
            Id = faction.Id,
            Name = faction.Name,
            Description = faction.Description
        };
    }

    public async Task<FactionDto> CreateAsync(FactionCreateDto dto)
    {
        var faction = new Faction
        {
            Name = dto.Name,
            Description = dto.Description
        };

        _context.Factions.Add(faction);
        await _context.SaveChangesAsync();

        return new FactionDto
        {
            Id = faction.Id,
            Name = faction.Name,
            Description = faction.Description
        };
    }

    public async Task<bool> UpdateAsync(int id, FactionUpdateDto dto)
    {
        var faction = await _context.Factions.FindAsync(id);
        if (faction == null) return false;

        faction.Name = dto.Name;
        faction.Description = dto.Description;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var faction = await _context.Factions.FindAsync(id);
        if (faction == null) return false;

        _context.Factions.Remove(faction);
        await _context.SaveChangesAsync();

        return true;
    }
}