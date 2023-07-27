using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KetoNificent.Models.Serving;
using KetoNificent.Models.User;

namespace KetoNificent.Models.Product;

public class ProductIndexVM
{
    [Required]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public virtual int UserId { get; set; }
    public virtual List<string> NameList { get; set; } = null!;
}