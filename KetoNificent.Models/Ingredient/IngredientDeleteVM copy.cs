using System.ComponentModel.DataAnnotations;

namespace KetoNificent.Models.Ingredient;

public class IngredientDeleteVM
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}