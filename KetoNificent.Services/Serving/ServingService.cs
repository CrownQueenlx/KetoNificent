using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Serving;
using KetoNificent.Models.User;
using KetoNificent.Services;

namespace KetoNificent.Services.Serving;

public class ServingService : IServingService
{
    private readonly AppDbContext _context;
    public ServingService(AppDbContext context)
    { _context = context; }

    public async Task<ServingEntity?> CreateServingAsync(ServingEntity request)
    {
        var entity = new ServingEntity()
        {
            Measurement = request.Measurement,
            Amount = request.Amount,
            IngredientId = request.IngredientId,
            // be certain that the builder waits for the IngredientId to be fetched so that it will be connected
            // IngredientId = await _context.FindAsync(Ingredient.id == ServingEntity.IngredientId),
            // ProductId = new() ServingEntity.ProductId
        };
        _context.Servings.Add(entity);
        var numOfChanges = await _context.SaveChangesAsync();
        if (numOfChanges == 1)
        {
            ServingEntity response = new()
            {
                Id = request.Id,
                Measurement = request.Measurement,
                Amount = request.Amount,
                // IngredientId = await _context.Ingredients.FirstOrDefault(Id),
                // ProductId = await _context.Products.FirstOrDefault(Id),
            };
            return response;
        }
        return null;
    }

    public async Task<bool> GetServingByNameAsync()
    {
        var serving = await _context.Servings.FindAsync();
        var numberOfChanges = await _context.SaveChangesAsync();
        if (numberOfChanges == 1)
        return true;
        return false;
    }

    public async Task<bool> UpdateServingByIdAsync(ServingEntity request)
    {
        var isValid = await _context.Servings.FindAsync();
        if (request != isValid)
        return false;
        if (request == isValid)
        return true;
        var serving = new ServingEntity();
        serving.Measurement = request.Measurement;
        serving.Amount = request.Amount;
        serving.IngredientId = request.IngredientId;
        serving.ProductId = request.ProductId;

        var numOfChanges = await _context.SaveChangesAsync();
         return numOfChanges == 1;
    }
    public async Task<bool> DeleteServingAsync(int servingId)
    {
        var servingEntity = await _context.Servings.FindAsync(servingId);

        if (servingEntity is null)
        return false;

        _context.Servings.Remove(servingEntity);
        return await _context.SaveChangesAsync() == 1;
    }
    // private Task<Serving.IngredientId> GetIngredientIdAsync()
    // {

    // }
}