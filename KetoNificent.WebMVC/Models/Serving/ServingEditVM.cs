namespace KetoNificent.Models.Serving;

public class ServingEditVM
{
    public int Id { get; set; }
    public string Measurement { get; set; } = string.Empty;
    public int Amount { get; set; }
    public int IngredientId { get; set; }
    public int ProductId { get; set; }
}