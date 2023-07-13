using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KetoNificent.Data.Entities;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }

    [MinLength(2), MaxLength(20)]
    public string Name { get; set; } = string.Empty; // name of the final product

    [ForeignKey(nameof(UserId))]
    public int User { get; set; }
    public UserEntity UserId { get; set; } = null!; //so that users can save their combinations

}