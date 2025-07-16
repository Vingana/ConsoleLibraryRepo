using Microsoft.AspNetCore.Identity;
using WebApp.DTO.Auth;
using WebApp.Services.JwtServices;

namespace WebApp.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;

    public AuthService(UserManager<User> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<IResult> RegisterUserAsync(RegisterDto model)
    {
        var user = new User { UserName = model.UserName, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return Results.BadRequest(new { error = errors });
        }

        var addRoleResult = await _userManager.AddToRoleAsync(user, "User");
        if (!addRoleResult.Succeeded)
        {
            var errors = string.Join("; ", addRoleResult.Errors.Select(e => e.Description));
            return Results.BadRequest(new { error = errors });
        }

        var token = await _jwtService.GenerateTokenAsync(user);

        return Results.Ok(new RegisterDtoResult
        {
            Message = "User created successfully.",
            Token = token
        });
    }

    public async Task<IResult> LoginUserAsync(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            return Results.Json(new { error = "Invalid username or password." }, statusCode: 401);
        }

        var token = await _jwtService.GenerateTokenAsync(user);

        return Results.Ok(new LoginDtoResult
        {
            Message = "Login successful.",
            Token = token
        });
    }
}