using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO.CharacterClass;
using WebApp.Services.CharacterClassServices;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterClassController : ControllerBase
{
    private readonly ICharacterClassService _service;

    public CharacterClassController(ICharacterClassService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<CharacterClassDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CharacterClassDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<CharacterClassDto>> Create(CharacterClassCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CharacterClassUpdateDto dto)
    {
        var success = await _service.UpdateAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}