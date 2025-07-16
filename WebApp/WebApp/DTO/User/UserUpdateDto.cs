﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.User;

public class UserUpdateDto
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    [Url(ErrorMessage = "Invalid URL format")]
    public string? ProfilePictureUrl { get; set; }
}