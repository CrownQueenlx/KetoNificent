using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace KetoNificent.Data.Entities;

public class UserEntity : IdentityUser<int>
{
    [Key]
    public int ID {get; set; } = new();
    public override int Id => ID;
    public string? Name { get; set; }
    public DateTime DateCreated { get; set; }
}