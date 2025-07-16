using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.User;

public class ChangeEmailDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string NewEmail { get; set; } = null!;
}