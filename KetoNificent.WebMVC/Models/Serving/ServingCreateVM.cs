using System.ComponentModel.DataAnnotations;

namespace KetoNificent.Models.Serving;

public class ServingCreateVM
{
    public string Measurement { get; set; } = string.Empty;
    public int Amount { get; set; }
    public int IngredientId { get; set; }
    public int ProductId { get; set; }
}