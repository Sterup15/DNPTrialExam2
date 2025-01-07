namespace WebApi.DataEntities;

public class StorageRoom
{
    public int Id { get; set; }
    public String Location { get; set; }
    public Dimensions Dimensions { get; set; }
    public List<Box> Boxes { get; set; }
    
}