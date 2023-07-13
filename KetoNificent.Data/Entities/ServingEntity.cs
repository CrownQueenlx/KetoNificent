using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KetoNificent.Data.Entities;

public class ServingEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Measurement { get; set; } = string.Empty; //type of measurement

    [Required]
    public int Amount { get; set; } //how many of the measurement
    
    [ForeignKey(nameof(ProductEntity.Id))]
    public ProductEntity ProductId { get; set; } = null!;
}