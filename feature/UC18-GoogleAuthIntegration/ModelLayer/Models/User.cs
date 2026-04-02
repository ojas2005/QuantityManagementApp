using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Username { get; set; } = string.Empty;
    
    public string? PasswordHash { get; set; }
    
    public string? GoogleId { get; set; }
    
    public string? ProfilePicture { get; set; }
    
    public string? FullName { get; set; }
    
    public string Role { get; set; } = "User";
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? LastLoginAt { get; set; }
    
    public bool IsActive { get; set; } = true;  // 👈 Default value set here
}
