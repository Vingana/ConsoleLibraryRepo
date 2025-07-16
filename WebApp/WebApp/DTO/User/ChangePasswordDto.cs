using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.User;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "Current password is required")]
    public string CurrentPassword { get; set; } = null!;

    [Required(ErrorMessage = "New password is required")]
    [MinLength(6, ErrorMessage = "Minimum password length is 6 characters")]
    public string NewPassword { get; set; } = null!;
}