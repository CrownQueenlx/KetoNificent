using KetoNificent.Data.Entities;
using KetoNificent.Models.Serving;
using KetoNificent.Models.User;

namespace KetoNificent.Services.Serving;

public interface IServingService
{
    // CREATE 
    Task<ServingEntity?> CreateServingAsync(ServingCreateVM request);
    // READ 
    Task<bool> GetServingByNameAsync();

    // UPDATE 
    Task<bool> UpdateServingByIdAsync(ServingEntity request);

    // DELETE
    Task<bool> DeleteServingAsync(int servingId);
}