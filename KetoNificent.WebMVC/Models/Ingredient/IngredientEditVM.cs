namespace KetoNificent.Models.Ingredient;

public class IngredientEditVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NCarb { get; set;} = string.Empty;
    public int? NCarbCt { get; set;}
    public int? FatCt { get; set; }
    public int? ProteinCt { get; set; }
    public string DefaultMeasurement { get; set; } = string.Empty;
    public int? DefaultAmount { get; set; }
}