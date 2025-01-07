using WebApi.DataEntities;

namespace WebApi.Services.StorageService;

public class StorageServiceImpl : IStorageService
{
    public List<StorageRoom> StorageRooms { get; set; }

    public StorageServiceImpl()
    {
        StorageRooms = new List<StorageRoom>();
        GenerateStorageRooms();
    }

    private int countAllBoxes()
    {
        int boxCount = 0;
        foreach (StorageRoom room in StorageRooms)
        {
            foreach (Box box in room.Boxes)
            {
                boxCount++;
            }
        }
        return boxCount;
    }


    private void GenerateStorageRooms()
    {
        StorageRooms.Add(new StorageRoom
        {
            Id = 1,
            Dimensions = new Dimensions(15, 15, 25),
            Boxes = new List<Box>()
            {
                new Box
                {
                    Id = 1,
                    Label = "Ost",
                    Dimensions = new Dimensions(15, 15, 15)
                },
                new Box
                {
                    Id = 2,
                    Label = "Rejer",
                    Dimensions = new Dimensions(15, 15, 15)
                }
            },
            Location = "Herning"
        });

        StorageRooms.Add(new StorageRoom
        {
            Id = 2,
            Dimensions = new Dimensions(13, 16, 24),
            Boxes = new List<Box>()
            {
                new Box
                {
                    Id = 3,
                    Label = "Mælk",
                    Dimensions = new Dimensions(12,12,12)
                },
                new Box
                {
                    Id = 4,
                    Label = "Kød",
                    Dimensions = new Dimensions(15,15,15)
                }
            },
            Location = "Horsens"
        });

        StorageRooms.Add(new StorageRoom
        {
            Id = 3,
            Dimensions = new Dimensions(16, 11, 22),
            Boxes = new List<Box>(),
            Location = "Skanderborg"
        });

        StorageRooms.Add(new StorageRoom
        {
            Id = 4,
            Dimensions = new Dimensions(25, 24, 35),
            Boxes = new List<Box>(),
            Location = "Silkeborg"
        });

        StorageRooms.Add(new StorageRoom
        {
            Id = 5,
            Dimensions = new Dimensions(18, 14, 23),
            Boxes = new List<Box>(),
            Location = "Aarlborg"
        });
    }

    public async Task<Box> AddBoxAsync(int id, Box box)
    {
        var room = StorageRooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            throw new ArgumentException($"No room found with id: {id}");
        }

        box.Id = countAllBoxes() + 1;
        room.Boxes.Add(box);

        return box;
    }

    public async Task<IEnumerable<Box>> GetBoxesAsync(int id)
    {
        var room = StorageRooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            throw new ArgumentException($"No room found with id: {id}");
        }

        if (room.Boxes.Count == 0)
        {
            throw new ArgumentException($"No boxes found in room with id: {id}");
        }
        
        return room.Boxes;
    }

    public async Task DeleteBoxAsync(int id, int boxId)
    {
        var room = StorageRooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            throw new ArgumentException($"No room found with id: {id}");
        }

        var box = room.Boxes.FirstOrDefault(b => b.Id == boxId);

        if (box == null)
        {
            throw new ArgumentException($"No box found with id: {id}");
        }
        
        room.Boxes.Remove(box);
    }

    public async Task<IEnumerable<StorageRoom>> GetRooms(int? minimumRoomVolume, int? maxNumberOfBoxes)
    {
        var filteredRooms = StorageRooms.AsEnumerable();

        if (minimumRoomVolume.HasValue)
        {
            filteredRooms = filteredRooms.Where(r => r.Dimensions.Volume()>=minimumRoomVolume.Value);
        }

        if (maxNumberOfBoxes.HasValue)
        {
            filteredRooms = filteredRooms.Where(r => r.Boxes.Count < maxNumberOfBoxes.Value);
        }

        return filteredRooms;
    }
}