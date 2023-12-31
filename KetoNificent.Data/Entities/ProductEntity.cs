﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KetoNificent.Data.Entities;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }

    [MinLength(2), MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public virtual UserEntity User { get; set; } = null!; //so that users can save their combinations
    public virtual ICollection<ServingEntity> Servings { get; set; } = new List<ServingEntity>();
}
