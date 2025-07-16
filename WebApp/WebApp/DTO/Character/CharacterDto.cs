namespace WebApp.DTO.Character;

public class CharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string CharacterClass { get; set; } = null!;
    public string Race { get; set; } = null!;
    public string Faction { get; set; } = null!;
    public string? Description { get; set; }
}
