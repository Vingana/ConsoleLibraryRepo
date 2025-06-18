using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO.User;
using WebApp.Services.AccountServices;

namespace WebApp.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetMe()
    {
        var user = await _service.GetCurrentUserAsync(User);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UserUpdateDto dto)
    {
        var result = await _service.UpdateCurrentUserAsync(User, dto);

        if (result.Succeeded)
            return NoContent();

        var usernameConflict = result.Errors.Any(e => e.Code == "DuplicateUserName");
        var emailConflict = result.Errors.Any(e => e.Code == "DuplicateEmail");

        if (usernameConflict || emailConflict)
        {
            return Conflict(result.Errors);
        }

        return BadRequest(result.Errors);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete()
    {
        var result = await _service.DeleteCurrentUserAsync(User);
        if (result.Succeeded)
            return NoContent();

        return NotFound(result.Errors);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        var result = await _service.ChangePasswordAsync(User, dto);
        if (result.Succeeded)
            return NoContent();

        return BadRequest(result.Errors.Any() ? result.Errors : new[] { new { Description = "Invalid current password or password does not meet requirements." } });
    }

    [HttpPost("request-email-change")]
    public async Task<IActionResult> RequestEmailChange(ChangeEmailDto dto)
    {
        var token = await _service.RequestEmailChangeAsync(User, dto.NewEmail);
        if (token == null)
            return NotFound();

        return Ok(new { Token = token });
    }

    [AllowAnonymous]
    [HttpGet("confirm-email-change")]
    public async Task<IActionResult> ConfirmEmailChange([FromQuery] int userId, [FromQuery] string newEmail, [FromQuery] string token)
    {
        var result = await _service.ConfirmEmailChangeAsync(userId, newEmail, token);
        if (result.Succeeded)
            return Ok("Email successfully confirmed.");

        return BadRequest(result.Errors);
    }

    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        var token = await _service.RequestPasswordResetAsync(dto);
        return Ok(new { Message = "If this email exists, a reset link was sent.", Token = token });
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
    {
        var result = await _service.ResetPasswordAsync(dto);
        if (result.Succeeded)
            return Ok("Password successfully reset.");

        return BadRequest(result.Errors);
    }

    [HttpPut("update-profile-picture")]
    public async Task<IActionResult> UpdateProfilePicture([FromForm] ProfilePicUploadDto dto)
    {
        if (dto.File == null || dto.File.Length == 0)
            return BadRequest("Файл відсутній або порожній.");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"user_{User.FindFirstValue(ClaimTypes.NameIdentifier)}_{Guid.NewGuid()}{Path.GetExtension(dto.File.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }

        var result = await _service.UpdateProfilePictureAsync(User, $"/uploads/{fileName}");

        if (result.Succeeded)
            return NoContent();

        var usernameConflict = result.Errors.Any(e => e.Code == "DuplicateUserName");
        var emailConflict = result.Errors.Any(e => e.Code == "DuplicateEmail");

        if (usernameConflict || emailConflict)
        {
            return Conflict(result.Errors);
        }

        return BadRequest(result.Errors);
    }
}