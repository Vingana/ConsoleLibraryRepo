using WebApp.DTO.Character;

namespace WebApp.Services.CharacterServices
{
    public interface ICharacterService
    {
        Task<int> CreateCharacterAsync(CharacterCreateDto dto, int userId);
        Task<IEnumerable<CharacterDto>> GetAllCharactersAsync(int userId);
        Task<CharacterDto?> GetCharacterByIdAsync(int id, int userId);
        Task<bool> UpdateCharacterAsync(int id, CharacterUpdateDto dto, int userId);
        Task<bool> DeleteCharacterAsync(int id, int userId);
    }
}
