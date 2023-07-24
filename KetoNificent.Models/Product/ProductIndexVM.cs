using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KetoNificent.Models.User;

namespace KetoNificent.Models.Product;

public class ProductIndexVM
{
    [Required]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [ForeignKey(nameof(UserDetail.UserId))]
    public virtual int User { get; set; }
    public virtual List<string> NameList { get; set; } = null!;
}