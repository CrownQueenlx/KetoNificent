using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace KetoNificent.Data.Entities;

public class UserEntity : IdentityUser<int>
{
    [Key]
    public virtual int UserId {get; set; } = new();
    public override int Id => UserId;
    // string email is inherited from the Microsoft Identity
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public virtual ICollection<ProductEntity> ProductEntities { get; set; } = new List<ProductEntity>();

}