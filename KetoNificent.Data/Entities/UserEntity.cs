using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace KetoNificent.Data.Entities;

public class UserEntity : IdentityUser<int>
{ 
    // string email, password is inherited from the Microsoft Identity
    public string Name { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public virtual ICollection<ProductEntity> ProductEntities { get; set; } = new List<ProductEntity>();
}