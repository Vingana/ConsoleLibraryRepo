using WebApp.DTO.Faction;

namespace WebApp.Services.FactionServices;

public interface IFactionService
{
    Task<List<FactionDto>> GetAllAsync();
    Task<FactionDto?> GetByIdAsync(int id);
    Task<FactionDto> CreateAsync(FactionCreateDto dto);
    Task<bool> UpdateAsync(int id, FactionUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}