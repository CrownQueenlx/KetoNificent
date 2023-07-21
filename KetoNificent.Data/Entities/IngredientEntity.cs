using System.ComponentModel.DataAnnotations;


namespace KetoNificent.Data.Entities;

public class IngredientEntity
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public string NCarb { get; set; } = string.Empty;
    public  int? NCarbCt { get; set; } //number of ncarbs
 
    // Fat name wil be hard coded
    public int? Fat { get; set; } //number of fat

    // Protein name wil be hard coded
    public int? Protein { get; set; } //number of protein 

    [Required]
    public string DefaultMeasurement { get; set; } = string.Empty;

    [Required]
    public int? DefaultAmount { get; set; }
    public virtual ICollection<ServingEntity> Servings { get; set; } = new List<ServingEntity>();

}
