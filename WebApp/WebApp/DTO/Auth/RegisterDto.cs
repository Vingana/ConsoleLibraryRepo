using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.Auth;

public class RegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Ім’я користувача має містити від 3 до 20 символів")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Некоректний формат email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль має містити щонайменше 6 символів")]
    public string Password { get; set; } = null!;
}