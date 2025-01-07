using WebApi.DataEntities;

namespace WebApi.Services.StorageService;

public interface IStorageService
{
    Task<Box> AddBoxAsync(int id, Box box);
    Task<IEnumerable<Box>> GetBoxesAsync(int id);
    Task DeleteBoxAsync(int id, int boxId);
    Task<IEnumerable<StorageRoom>> GetRooms(int? minimumRoomVolume, int? maxNumberOfBoxes);
}