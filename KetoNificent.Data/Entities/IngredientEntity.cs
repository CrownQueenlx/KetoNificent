using System.ComponentModel.DataAnnotations;

namespace KetoNificent.Data.Entities;

public class IngredientEntity
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public string NCarb { get; set; } = string.Empty;
    public  int NCarbCt { get; set; } //number of ncarbs

    public string Fat { get; set; } = string.Empty;
    public int FatCt { get; set; } //number of fat

    public string Protein { get; set; } = string.Empty;
    public int ProteinCt { get; set; } //number of protein 
}