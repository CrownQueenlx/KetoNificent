using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Ingredient;

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
        _dbContext.Ingredient.Add(IngredEntity);
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

    public Task<IngredientEntity> GetIngredientByIdAsync(int ingredId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateIngredientByIdAsync(IngredientModel request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteIngredientByIdAsync(int ingredId)
    {
        throw new NotImplementedException();
    }
}