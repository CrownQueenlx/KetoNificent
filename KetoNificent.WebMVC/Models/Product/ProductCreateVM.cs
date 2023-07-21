using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KetoNificent.Models.User;

namespace KetoNificent.Models.Product;

public class ProductCreateVM
{
    public string Name { get; set; } = string.Empty;

    [ForeignKey(nameof(UserDetail.UserId))]
    public virtual int User { get; set; }
}