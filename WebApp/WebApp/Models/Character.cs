namespace WebApp.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; }
    public int CharacterClassId { get; set; }
    public CharacterClass CharacterClass { get; set; }
    public int RaceId { get; set; }
    public Race Race { get; set; }
    public int FactionId { get; set; }
    public Faction Faction { get; set; }
    public string? Description { get; set; }
}