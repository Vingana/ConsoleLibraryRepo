using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.DTO.Race;
using WebApp.Models;

namespace WebApp.Services.RaceServices;

public class RaceService : IRaceService
{
    private readonly ApplicationContext _context;

    public RaceService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<RaceDto>> GetAllAsync()
    {
        return await _context.Races
            .Select(r => new RaceDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            })
            .ToListAsync();
    }

    public async Task<RaceDto?> GetByIdAsync(int id)
    {
        var race = await _context.Races.FindAsync(id);
        return race == null ? null : new RaceDto
        {
            Id = race.Id,
            Name = race.Name,
            Description = race.Description
        };
    }

    public async Task<RaceDto> CreateAsync(RaceCreateDto dto)
    {
        var race = new Race
        {
            Name = dto.Name,
            Description = dto.Description
        };

        _context.Races.Add(race);
        await _context.SaveChangesAsync();

        return new RaceDto
        {
            Id = race.Id,
            Name = race.Name,
            Description = race.Description
        };
    }

    public async Task<bool> UpdateAsync(int id, RaceUpdateDto dto)
    {
        var race = await _context.Races.FindAsync(id);
        if (race == null) return false;

        race.Name = dto.Name;
        race.Description = dto.Description;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var race = await _context.Races.FindAsync(id);
        if (race == null) return false;

        _context.Races.Remove(race);
        await _context.SaveChangesAsync();

        return true;
    }
}