using WebApp.DTO.Race;

namespace WebApp.Services.RaceServices;

public interface IRaceService
{
    Task<List<RaceDto>> GetAllAsync();
    Task<RaceDto?> GetByIdAsync(int id);
    Task<RaceDto> CreateAsync(RaceCreateDto dto);
    Task<bool> UpdateAsync(int id, RaceUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}