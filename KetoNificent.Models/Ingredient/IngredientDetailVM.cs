using System.ComponentModel.DataAnnotations;

namespace KetoNificent.Models.Ingredient;

public class IngredientDetailVM
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public string NCarb { get; set; } = string.Empty;
    public int? NCarbCt { get; set; } //number of ncarbs
    public int? Fat { get; set; } //number of fat
    // Fat name will be hardcoded
    public int? Protein { get; set; }  //number of protein 
    // Protein name will be hardcoded
    [Required]
    public string DefaultMeasurement { get; set; } = string.Empty;
    
    [Required]
    public int? DefaultAmount { get; set; }
    public virtual int Serving { get; set; }
}