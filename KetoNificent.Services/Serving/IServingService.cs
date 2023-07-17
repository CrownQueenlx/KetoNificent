using KetoNificent.Data.Entities;
using KetoNificent.Models.Serving;
using KetoNificent.Models.User;

namespace KetoNificent.Services.Serving;

public interface IServingService
{
    // CREATE 
    Task<ServingModel> CreateServingAsync();
    // READ 
    Task<bool> GetServingByNameAsync();

    // UPDATE 
    Task<ServingEntity> UpdateServingByIdAsync();

    // DELETE
    Task<bool> DeleteServingAsync();
}