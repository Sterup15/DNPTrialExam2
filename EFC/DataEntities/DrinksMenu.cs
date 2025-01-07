namespace EFC.DataEntities;

public class DrinksMenu
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool ContainsAlcohol { get; set; }
    public DateTime AvailableFrom { get; set; }

    public List<Drink> Drinks { get; set; } = new();

    public DrinksMenu()
    {
    }
}