using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KetoNificent.Models.Ingredient;
using KetoNificent.Models.Product;

namespace KetoNificent.Models.Serving;

public class ServingModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Measurement { get; set; } = string.Empty; //type of measurement

    [Required]
    public int Amount { get; set; } //how many of the measurement
    
    [ForeignKey(nameof(IngredientModel.Id))]
    public IngredientModel IngredientId { get; set; } = null!;
    
    [ForeignKey(nameof(ProductModel.Id))]
    public ProductModel ProductId { get; set; } = null!;
}