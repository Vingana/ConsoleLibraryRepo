using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.User;

public class ResetPasswordDto
{
    [Required(ErrorMessage = "User ID is required")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Token is required")]
    public string Token { get; set; } = null!;

    [Required(ErrorMessage = "New password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string NewPassword { get; set; } = null!;
}