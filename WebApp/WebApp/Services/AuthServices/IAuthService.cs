using WebApp.DTO.Auth;

namespace WebApp.Services.AuthServices;

public interface IAuthService
{
    Task<IResult> RegisterUserAsync(RegisterDto dto);
    Task<IResult> LoginUserAsync(LoginDto dto);
}