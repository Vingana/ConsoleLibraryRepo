using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.DTO.Character;
using WebApp.Services.CharacterServices;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CharactersController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharactersController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    private int? GetUserId()
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(userIdStr, out var id) ? id : null;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CharacterDto>>> GetAllCharacters()
    {
        var userId = GetUserId();
        if (userId == null)
            return Unauthorized();

        var characters = await _characterService.GetAllCharactersAsync(userId.Value);
        return Ok(characters);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CharacterDto>> GetCharacterById(int id)
    {
        var userId = GetUserId();
        if (userId == null)
            return Unauthorized();

        var character = await _characterService.GetCharacterByIdAsync(id, userId.Value);

        if (character == null)
            return NotFound();

        return Ok(character);
    }

    [HttpPost]
    public async Task<ActionResult<CharacterDto>> CreateCharacter([FromBody] CharacterCreateDto dto)
    {
        var userId = GetUserId();
        if (userId == null)
            return Unauthorized();

        var newCharacterId = await _characterService.CreateCharacterAsync(dto, userId.Value);

        var createdCharacter = await _characterService.GetCharacterByIdAsync(newCharacterId, userId.Value);

        if (createdCharacter == null)
            return NotFound();

        return CreatedAtAction(nameof(GetCharacterById), new { id = createdCharacter.Id }, createdCharacter);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacter(int id, [FromBody] CharacterUpdateDto dto)
    {
        var userId = GetUserId();
        if (userId == null)
            return Unauthorized();

        var result = await _characterService.UpdateCharacterAsync(id, dto, userId.Value);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
        var userId = GetUserId();
        if (userId == null)
            return Unauthorized();

        var result = await _characterService.DeleteCharacterAsync(id, userId.Value);

        if (!result)
            return NotFound();

        return NoContent();
    }
}