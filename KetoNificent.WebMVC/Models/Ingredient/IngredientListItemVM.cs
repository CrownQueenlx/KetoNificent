namespace KetoNificent.Models.Ingredient;

public class IngredientListItemVM
{
    public string Name { get; set; } = string.Empty;
    public int NCarbCt { get; set;}
    public string DefaultMeasurement { get; set; } = string.Empty;
    public int DefaultAmount { get; set; }

}