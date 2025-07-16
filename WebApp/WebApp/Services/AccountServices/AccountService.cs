using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApp.DTO.User;

namespace WebApp.Services.AccountServices;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AccountService> _logger;

    public AccountService(UserManager<User> userManager, ILogger<AccountService> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<UserDto?> GetCurrentUserAsync(ClaimsPrincipal userPrincipal)
    {
        var user = await _userManager.GetUserAsync(userPrincipal);
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            Username = user.UserName!,
            Email = user.Email!,
            CreatedAt = user.CreatedAt,
            ProfilePictureUrl = user.ProfilePictureUrl
        };
    }

    public async Task<IdentityResult> UpdateCurrentUserAsync(ClaimsPrincipal userPrincipal, UserUpdateDto dto)
    {
        var user = await _userManager.GetUserAsync(userPrincipal);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = "User not found."
            });
        }

        user.UserName = dto.Username.Trim();
        user.Email = dto.Email.Trim();
        user.ProfilePictureUrl = dto.ProfilePictureUrl?.Trim();

        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteCurrentUserAsync(ClaimsPrincipal userPrincipal)
    {
        var user = await _userManager.GetUserAsync(userPrincipal);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = "User not found."
            });
        }

        if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.ProfilePictureUrl.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting profile picture at path {FilePath}", filePath);
                }
            }
        }
        return await _userManager.DeleteAsync(user);
    }


    public async Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal userPrincipal, ChangePasswordDto dto)
    {
        var user = await _userManager.GetUserAsync(userPrincipal);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = "User not found."
            });
        }

        return await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
    }

    public async Task<string?> RequestEmailChangeAsync(ClaimsPrincipal userPrincipal, string newEmail)
    {
        var user = await _userManager.GetUserAsync(userPrincipal);
        if (user == null) return null;

        var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);

        return token;
    }

    public async Task<IdentityResult> ConfirmEmailChangeAsync(int userId, string newEmail, string token)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
        return result;
    }

    public async Task<string?> RequestPasswordResetAsync(ForgotPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return null;

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        return token;
    }

    public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        return result;
    }
    public async Task<IdentityResult> UpdateProfilePictureAsync(ClaimsPrincipal userPrincipal, string profilePictureUrl)
    {
        var user = await _userManager.GetUserAsync(userPrincipal);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        user.ProfilePictureUrl = profilePictureUrl?.Trim();

        return await _userManager.UpdateAsync(user);
    }
}