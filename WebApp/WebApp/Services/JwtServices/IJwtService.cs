namespace WebApp.Services.JwtServices;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(User user);
}
