using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KetoNificent.Data.Entities;

namespace KetoNificent.Models.Product;

public class ProductModel
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [ForeignKey(nameof(Id))]
    public int UserId { get; set; }
}