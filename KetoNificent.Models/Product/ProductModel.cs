using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KetoNificent.Models.User;

namespace KetoNificent.Models.Product;

public class ProductModel
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty; //the name of the final product

    [ForeignKey(nameof(UserId))]
    public int User { get; set; }
    public virtual UserDetail UserId { get; set; } = null!;

    public virtual int? Serving { get; set; }
}