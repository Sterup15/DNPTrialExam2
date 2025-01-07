namespace WebApi.DataEntities;

public class Dimensions
{
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Dimensions(int length, int width, int height)
    {
        Length = length;
        Width = width;
        Height = height;
    }

    public int Volume()
    {
        return Length * Width * Height;
    }
}