namespace WebApp.DTO.User;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string? ProfilePictureUrl { get; set; }
}