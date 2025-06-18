using WebApp.DTO.CharacterClass;

namespace WebApp.Services.CharacterClassServices;

public interface ICharacterClassService
{
    Task<List<CharacterClassDto>> GetAllAsync();
    Task<CharacterClassDto?> GetByIdAsync(int id);
    Task<CharacterClassDto> CreateAsync(CharacterClassCreateDto dto);
    Task<bool> UpdateAsync(int id, CharacterClassUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}