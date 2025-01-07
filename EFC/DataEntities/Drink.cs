namespace EFC.DataEntities;

public class Drink
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double AlcoholPercentage { get; set; }
    public bool ContainsUmbrella { get; set; }
    
    public int DrinksMenuId { get; set; }
    public DrinksMenu DrinksMenu { get; set; }
}