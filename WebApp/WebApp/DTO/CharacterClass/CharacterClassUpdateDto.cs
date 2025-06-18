using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.CharacterClass;

public class CharacterClassUpdateDto
{
    [Required(ErrorMessage = "Class name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
    public string Name { get; set; } = null!;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }
}