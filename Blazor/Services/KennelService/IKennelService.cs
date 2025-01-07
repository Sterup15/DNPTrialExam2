using Blazor.DataEntities;

namespace Blazor.Services.KennelService;

public interface IKennelService
{
    Task<Dog> RegisterDogAsync(Dog dog);
    Task<List<Dog>> GetDogsAsync();
}