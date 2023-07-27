using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.Ingredient;
using KetoNificent.Models.Serving;
using KetoNificent.Models.User;
using KetoNificent.Services;
using Microsoft.EntityFrameworkCore;

namespace KetoNificent.Services.Serving;

public class ServingService : IServingService
{
    private readonly AppDbContext _context;
    public ServingService(AppDbContext context)
    { _context = context; }

    // Post: Create
    public async Task<ServingEntity?> CreateServingAsync(ServingCreateVM model)
    {
        var entity = new ServingEntity()
        {
            Measurement = model.Measurement,
            Amount = model.Amount,
            IngredientId = model.IngredientId,
            //these items do not need to be awaited from the *~Entity context 
            //because the ForeignKey handles the connection to the information
            ProductId = model.ProductId,
        };
        _context.Servings.Add(entity);
        var numOfChanges = await _context.SaveChangesAsync();
        if (numOfChanges == 1)
        {
            ServingEntity response = new()
            {
                Id = model.Id,
                Measurement = model.Measurement,
                Amount = model.Amount,
                IngredientId = model.IngredientId,
                ProductId = model.ProductId,
            };
            return response;
        }
        return null;
    }

    // Get: Read By Id
    public async Task<ServingDetailVM?> GetServingByIdAsync(int model)
    {
        //get serving entity
        //get corresponding ingredientId s
        // get the names for those ingredients
        // return a list of names
         var serving = await _context.Servings
         .FirstOrDefaultAsync(y => y.Id == model);
         return serving is null ? null : new ServingDetailVM()
        {
            Id = serving.Id,
            Measurement = serving.Measurement,
            Amount = serving.Amount,
            IngredientId = serving.IngredientId,
            ProductId = serving.ProductId
        };
        // return (serving);
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