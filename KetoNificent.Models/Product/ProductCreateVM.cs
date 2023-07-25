using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KetoNificent.Models.User;

namespace KetoNificent.Models.Product;

public class ProductCreateVM
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [ForeignKey(nameof(UserDetailVM.UserId))]
    public virtual int User { get; set; }
}