using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebApp.DTO.User;

namespace WebApp.Services.AccountServices;

public interface IAccountService
{
    Task<UserDto?> GetCurrentUserAsync(ClaimsPrincipal user);
    Task<IdentityResult> UpdateCurrentUserAsync(ClaimsPrincipal userPrincipal, UserUpdateDto dto);
    Task<IdentityResult> DeleteCurrentUserAsync(ClaimsPrincipal user);
    Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal user, ChangePasswordDto dto);
    Task<string?> RequestEmailChangeAsync(ClaimsPrincipal userPrincipal, string newEmail);
    Task<IdentityResult> ConfirmEmailChangeAsync(int userId, string newEmail, string token);
    Task<string?> RequestPasswordResetAsync(ForgotPasswordDto dto);
    Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto dto);
    Task<IdentityResult> UpdateProfilePictureAsync(ClaimsPrincipal userPrincipal, string profilePictureUrl);
}