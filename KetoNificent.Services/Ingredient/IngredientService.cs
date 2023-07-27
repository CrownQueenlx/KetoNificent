using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Ingredient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Services.Ingredient;

public class IngredientService : IIngredientService
{
    private readonly AppDbContext _dbContext;
    public IngredientService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IngredientEntity?> CreateIngredientAsync(IngredientCreateVM model)
    {
        var IngredEntity = new IngredientEntity()
        {
            Id = model.Id,
            Name = model.Name,
            NCarb = model.NCarb,
            Fat = model.Fat,
            Protein = model.Protein,
            DefaultMeasurement = model.DefaultMeasurement,
            DefaultAmount = model.DefaultAmount
        };
        _dbContext.Ingredients.Add(IngredEntity);
        var numberOfChanges = await _dbContext.SaveChangesAsync();
        if (numberOfChanges == 1)
        {
            Data.Entities.IngredientEntity response = new()
            {
                Id = model.Id,
                Name = model.Name,
                NCarb = model.NCarb,
                Fat = model.Fat,
                Protein = model.Protein,
                DefaultMeasurement = model.DefaultMeasurement,
                DefaultAmount = model.DefaultAmount
            };
            return response;
        }
        return null;

    }

    //Index is used as a brief overview here
    public async Task<List<IngredientIndexVM>> GetIngredientsAsync()
    {
        List<IngredientIndexVM> ingredModel = await _dbContext.Ingredients
               .Select(y => new IngredientIndexVM
               {
                   Name = y.Name,
                   NCarb = y.NCarb,
                   NCarbCt = y.NCarbCt,
                   DefaultMeasurement = y.DefaultMeasurement,
                   DefaultAmount = y.DefaultAmount
               })
                .ToListAsync();

        return (ingredModel);
    }

    // Get IngredientNames as a list
    public async Task<List<IngredientListItemVM>> GetIngredientNamesByServingAsync(ServingEntity model)
    {
        if (model is null)
        {
            return null;
        }

        List<IngredientListItemVM> vm = await _dbContext.Ingredients
        .Select(i => new IngredientListItemVM
        {
            Name = i.Name
        })
        .ToListAsync();
        return (vm);
    }


    public async Task<List<IngredientDetailVM>> GetIngredientDetailsAsync(IngredientDetailVM model)
    {
        List<IngredientDetailVM> ingredModel = await _dbContext.Ingredients
               .Select(y => new IngredientDetailVM
               {
                   Id = model.Id,
                   Name = model.Name,
                   NCarb = model.NCarb,
                   Fat = model.Fat,
                   Protein = model.Protein,
                   DefaultMeasurement = model.DefaultMeasurement,
                   DefaultAmount = model.DefaultAmount
               })
                .ToListAsync();

        return (ingredModel);
    }
    public async Task<IngredientDetailVM?> GetIngredientByIdAsync(int model)
    {
        // find the first Ingredient with the given Id
        var ingredientEntity = await _dbContext.Ingredients
        .FirstOrDefaultAsync(y => y.Id == model);

        // If model is null -> return null, otherwise 
        // initialize and return new
        return ingredientEntity is null ? null : new IngredientDetailVM
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

    public async Task<bool> UpdateIngredientByIdAsync(IngredientEditVM model)
    {
        // find the first Ingredient by Id
        var ingredEntity = await _dbContext.Ingredients.FindAsync(model.Id);
        //    check if null
        if (ingredEntity is null)
            return false;

        // update entity's properties
        ingredEntity.Name = model.Name;
        ingredEntity.NCarb = model.NCarb;
        ingredEntity.Fat = model.Fat;
        ingredEntity.Protein = model.Protein;
        ingredEntity.DefaultMeasurement = model.DefaultMeasurement;
        ingredEntity.DefaultAmount = model.DefaultAmount;

        // save the changes to the database, capture the number of rows changed
        var numOfChanges = await _dbContext.SaveChangesAsync();

        // put numOfChanges equal to one to show if only one row was updated
        return numOfChanges == 1;
    }

    public async Task<bool> DeleteIngredientByIdAsync(int model)
    {
        // find the Ingredient by the given Id
        var ingredientEntity = await _dbContext.Ingredients.FindAsync(model);

        // validate that the Ingredient exists
        if (ingredientEntity == null)
            return false;


        // remove the ingredient from the dbcontext and assert that one change was saved
        _dbContext.Ingredients.Remove(ingredientEntity);
        return false;
    }
}