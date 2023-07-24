using KetoNificent.Data.Entities;
using KetoNificent.Models.Ingredient;

namespace KetoNificent.Services.Ingredient;

public interface IIngredientService
{
    // Create
    Task<IngredientEntity?> CreateIngredientAsync(IngredientCreateVM ingredient);
    // Get
    Task<IngredientDetailVM?> GetIngredientByIdAsync(int ingredId);
    // Update
    Task<bool> UpdateIngredientByIdAsync(IngredientEditVM request);
    // Delete
    Task<bool> DeleteIngredientByIdAsync(int ingredId);
    Task<List<IngredientIndexVM>> GetIngredientsAsync();
}