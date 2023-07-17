using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Ingredient;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Services.Ingredient;

public class IngredientService : IIngredientService
{
    private readonly AppDbContext _dbContext;
    public IngredientService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IngredientEntity> CreateIngredientAsync(IngredientModel request)
    {
        var IngredEntity = new IngredientEntity()
        {
            Id = request.Id,
            Name = request.Name,
            NCarb = request.NCarb,
            Fat = request.Fat,
            Protein = request.Protein,
            DefaultMeasurement = request.DefaultMeasurement,
            DefaultAmount = request.DefaultAmount
        };
        _dbContext.Ingredients.Add(IngredEntity);
        var numberOfChanges = await _dbContext.SaveChangesAsync();
        if (numberOfChanges == 1)
        {
            IngredientEntity response = new()
            {
                Id = request.Id,
                Name = request.Name,
                NCarb = request.NCarb,
                Fat = request.Fat,
                Protein = request.Protein,
                DefaultMeasurement = request.DefaultMeasurement,
                DefaultAmount = request.DefaultAmount
            };
            return response;
        }
        return null;

    }

    public async Task<IngredientModel?> GetIngredientByIdAsync(int ingredId)
    {
        // find the first Ingredient with the given Id
        var ingredientEntity = await _dbContext.Ingredients
        .FirstOrDefaultAsync(y => y.Id == ingredId);

        // If ingredId is null -> return null, otherwise 
        // initialize and return new
        return ingredientEntity is null ? null : new IngredientModel
        {
            Id = ingredientEntity.Id,
            Name = ingredientEntity.Name,
            NCarb = ingredientEntity.NCarb,
            Fat = ingredientEntity.Fat,
            Protein = ingredientEntity.Protein,
            DefaultMeasurement = ingredientEntity.DefaultMeasurement,
            DefaultAmount = ingredientEntity.DefaultAmount
        };
    }

    public async Task<bool> UpdateIngredientByIdAsync(IngredientModel request)
    {
        // find the first Ingredient by Id
        var ingredEntity = await _dbContext.Ingredients.FindAsync(request.Id);
        //    check if null
        if (ingredEntity is null)
        return false;

        // update entity's properties
        ingredEntity.Name = request.Name;
        ingredEntity.NCarb = request.NCarb;
        ingredEntity.Fat = request.Fat;
        ingredEntity.Protein = request.Protein;
        ingredEntity.DefaultMeasurement = request.DefaultMeasurement;
        ingredEntity.DefaultAmount =request.DefaultAmount;

        // save the changes to the database, capture the number of rows changed
        var numOfChanges = await _dbContext.SaveChangesAsync();

        // put numOfChanges equal to one to show if only one row was updated
        return numOfChanges == 1;
    }
    
    public async Task<bool> DeleteIngredientByIdAsync(int ingredId)
    {
        // find the Ingredient by the given Id
        var ingredientEntity = await _dbContext.Ingredients.FindAsync(ingredId);
        // remove the ingredient from the dbcontext and assert that one change was saved
        _dbContext.Ingredients.Remove(ingredientEntity);
        return false;
    }
}