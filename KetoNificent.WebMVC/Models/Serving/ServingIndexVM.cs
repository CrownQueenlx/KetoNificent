namespace KetoNificent.Models.Serving;

public class ServingIndexVM
{
    public int Id { get; set; }
    public string Measurement { get; set; } = string.Empty;
    public int? Amount { get; set; }
}