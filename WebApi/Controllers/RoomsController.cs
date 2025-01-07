using Microsoft.AspNetCore.Mvc;
using WebApi.DataEntities;
using WebApi.Services.StorageService;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IStorageService StorageService;

    public RoomsController(IStorageService storageService)
    {
        StorageService = storageService;
    }
    
    [HttpPost("{id:int}/boxes")]
    public async Task<ActionResult<Box>> AddBoxToStorageRoom([FromRoute] int id, [FromBody] Box box)
    {
        Box AddedBox = await StorageService.AddBoxAsync(id, box);
        return AddedBox;
    }
    
    [HttpGet("{id}/boxes")]
    public async Task<ActionResult<IEnumerable<Box>>> GetBoxes(int id)
    {
        IEnumerable<Box> boxes = await StorageService.GetBoxesAsync(id);
        if (boxes == null || !boxes.Any())
        {
            return NotFound($"No boxes found for room with ID {id}.");
        }

        return Ok(boxes);
    }

    [HttpDelete("{id}/boxes/{boxId}")]
    public async Task DeleteBox(int id, int boxId)
    {
        await StorageService.DeleteBoxAsync(id, boxId);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StorageRoom>>> GetRooms([FromQuery] int? minimumRoomVolume,
        [FromQuery] int? maxNumberOfBoxes)
    {
        var rooms = await StorageService.GetRooms(minimumRoomVolume, maxNumberOfBoxes);
        return Ok(rooms);
    }
}