using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.Character;

public class CharacterCreateDto
{
    [Required(ErrorMessage = "Character name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Please select a character class")]
    public int CharacterClassId { get; set; }

    [Required(ErrorMessage = "Please select a race")]
    public int RaceId { get; set; }

    [Required(ErrorMessage = "Please select a faction")]
    public int FactionId { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }
}