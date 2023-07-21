using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KetoNificent.Models.Ingredient;

public class IngredientModel
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public string NCarb { get; set; } = string.Empty;
    public int? NCarbCt { get; set; } //number of ncarbs

   // Fat name will be hard coded
    public int? Fat { get; set; } //number of fat

   // Protein name will be hard coded
    public int? Protein { get; set; } //number of protein 

    [Required]
    public string DefaultMeasurement { get; set; } = string.Empty;
    
    [Required]
    public int? DefaultAmount { get; set; }
    public virtual int Serving { get; set; }
}