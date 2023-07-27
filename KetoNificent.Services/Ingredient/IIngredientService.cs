using KetoNificent.Data.Entities;
using KetoNificent.Models.Ingredient;

namespace KetoNificent.Services.Ingredient;

public interface IIngredientService
{
    // Create
    Task<IngredientEntity?> CreateIngredientAsync(IngredientCreateVM ingredient);
    // Get index overview
    Task<IngredientDetailVM?> GetIngredientByIdAsync(int ingredId);
    Task<List<IngredientIndexVM>> GetIngredientsAsync();

    Task<List<IngredientListItemVM>> GetIngredientNamesByServingAsync(ServingEntity model);

    //Get detail view
    Task<List<IngredientDetailVM>> GetIngredientDetailsAsync(IngredientDetailVM model);
    // Update
    Task<bool> UpdateIngredientByIdAsync(IngredientEditVM request);
    // Delete
    Task<bool> DeleteIngredientByIdAsync(int ingredId);
}