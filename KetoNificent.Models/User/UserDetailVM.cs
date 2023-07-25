namespace KetoNificent.Models.User;

public class UserDetailVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public  string Email { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}