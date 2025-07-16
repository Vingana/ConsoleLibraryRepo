namespace WebApp.Models;

public class Faction
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Character> Characters { get; set; } = [];
}