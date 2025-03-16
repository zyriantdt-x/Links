using System.ComponentModel.DataAnnotations;

namespace Links.Shared.Identity;
public class RegisterRequest {
    [Required( ErrorMessage = "Username is required" )]
    public string Username { get; set; } = String.Empty;

    [EmailAddress]
    [Required( ErrorMessage = "Email is required" )]
    public string Email { get; set; } = String.Empty;

    [Required( ErrorMessage = "Password is required" )]
    public string Password { get; set; } = String.Empty;
}
