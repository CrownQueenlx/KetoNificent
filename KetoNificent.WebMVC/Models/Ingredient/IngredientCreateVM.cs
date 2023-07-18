namespace KetoNificent.Models.Ingredient;

public class IngredientCreateVM
{
    public string Name { get; set; } = string.Empty;
    public int NCarbCt { get; set;}
    public int FatCt { get; set; }
    public int ProteinCt { get; set; }
    public string DefaultMeasurement { get; set; } = string.Empty;
    public int DefaultAmount { get; set; }
}