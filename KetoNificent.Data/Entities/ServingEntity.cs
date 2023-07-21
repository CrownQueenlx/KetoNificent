﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KetoNificent.Data.Entities;

public class ServingEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Measurement { get; set; } = string.Empty;

    [Required]
    public int? Amount { get; set; }
    public int ProductId { get; set; }

    [ForeignKey(nameof(IngredientEntity.Id))]
    public int IngredientId { get; set; }
    public virtual IngredientEntity? Ingredent { get; set; } = null!;

    [ForeignKey(nameof(Product))]
    public int productNum { get; set; }
    public virtual ProductEntity? Product { get; set; }
}
