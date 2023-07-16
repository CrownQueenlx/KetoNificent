using KetoNificent.Data.Entities;
using KetoNificent.Models.Ingredient;

namespace KetoNificent.Services.Ingredient;

public interface IIngredientService
{
    // Create
    Task<IngredientEntity> CreateIngredientAsync(IngredientModel request);
    // Get
    Task<IngredientEntity> GetIngredientByIdAsync(int ingredId);
    // Update
    Task<bool> UpdateIngredientByIdAsync(IngredientModel request);
    // Delete
    Task<bool> DeleteIngredientByIdAsync(int ingredId);
}