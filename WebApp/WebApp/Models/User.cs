using WebApp.Models;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? ProfilePictureUrl { get; set; }
    public ICollection<Character> Characters { get; set; } = [];
}