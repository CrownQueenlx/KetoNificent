namespace KetoNificent.Models.Ingredient;

public class IngredientDetailVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NCarb { get; set; } = string.Empty;
    public int? NCarbCt { get; set; }
    public int? FatCt { get; set; }
    // Fat name will be hardcoded
    public int? ProteinCt { get; set; }
    // Protein name will be hardcoded
    public string DefaultMeasurement { get; set; } = string.Empty;
    public int? DefaultAmount { get; set; }
}