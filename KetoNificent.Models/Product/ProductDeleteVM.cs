using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KetoNificent.Models.User;

namespace KetoNificent.Models.Product;

public class ProductDeleteVM
{
    [Required]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [ForeignKey(nameof(UserDetailVM.UserId))]
    public virtual int User { get; set; }
}