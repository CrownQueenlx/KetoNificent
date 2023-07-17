using System.ComponentModel.DataAnnotations;

namespace KetoNificent.Models.User;

public class UserLogins
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}