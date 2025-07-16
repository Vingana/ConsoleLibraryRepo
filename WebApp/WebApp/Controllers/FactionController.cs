using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO.Faction;
using WebApp.Services.FactionServices;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FactionController : ControllerBase
{
    private readonly IFactionService _service;

    public FactionController(IFactionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<FactionDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FactionDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<FactionDto>> Create(FactionCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, FactionUpdateDto dto)
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