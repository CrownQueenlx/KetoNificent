using System.ComponentModel.DataAnnotations;

namespace KetoNificent.Models.Serving;

public class ServingDetailVM
{
    public int Id { get; set; }

    [Required]
    public string Measurement { get; set; } = string.Empty;

    [Required]
    public int? Amount { get; set; }
    public virtual int IngredientId { get; set; }
    public virtual int ProductId { get; set; }
}