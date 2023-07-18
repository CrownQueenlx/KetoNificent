namespace KetoNificent.Models.Ingredient;

public class IngredientEditVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int NCarbCt { get; set;}
    public string NCarb { get; set;} = string.Empty;
    public int FatCt { get; set; }
    public string Fat { get; set; } = string.Empty;
    public int ProteinCt { get; set; }
    public string Protein { get; set; } = string.Empty;
    public string DefaultMeasurement { get; set; } = string.Empty;
    public int DefaultAmount { get; set; }
}