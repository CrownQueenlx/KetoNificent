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

// Post: Create
    public async Task<ServingEntity?> CreateServingAsync(ServingCreateVM request)
    {
        var entity = new ServingEntity()
        {
            Measurement = request.Measurement,
            Amount = request.Amount,
            IngredientId = request.IngredientId, 
            //these items do not need to be awaited from the *~Entity context 
            //because the ForeignKey handles the connection to the information
            ProductId = request.ProductId,
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
                IngredientId = request.IngredientId,
                ProductId = request.ProductId,
            };
            return response;
        }
        return null;
    }

// Get: Read By Id
    public async Task<bool> GetServingByNameAsync()
    {
        var serving = await _context.Servings.FindAsync();
        var numberOfChanges = await _context.SaveChangesAsync();
        if (numberOfChanges == 1)
        return true;
        return false;
    }

    // Update
    public async Task<bool> UpdateServingByIdAsync(ServingEntity request)
    {
        var isValid = await _context.Servings.FindAsync(request);
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


    // Delete: by Id
    public async Task<bool> DeleteServingAsync(int servingId)
    {
        var servingEntity = await _context.Servings.FindAsync(servingId);

        if (servingEntity is null)
        return false;

        _context.Servings.Remove(servingEntity);
        return await _context.SaveChangesAsync() == 1;
    }
}