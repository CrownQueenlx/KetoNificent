using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KetoNificent.Models.Serving;
using KetoNificent.Models.User;

namespace KetoNificent.Models.Product;

public class ProductDetailVM
{
    [Required]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty; //the name of the final product
    
    [ForeignKey(nameof(UserDetail.UserId))]
    public virtual int User { get; set; }
    public virtual UserDetail UserId { get; set; } = null!;

    public virtual ICollection<ServingDetailVM> Servings { get; set; } = new List<ServingDetailVM>();
}