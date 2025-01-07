using EFC.DatabaseContext;
using EFC.DataEntities;

namespace EFC;

class Program
{
    static async Task Main(string[] args)
    {
        //Initialize Database access, as we don't have middleware that does dependency injection for us
        MenuContext menuContext = new MenuContext();
        DataAccess dataAccess = new DataAccess(menuContext);
        
        //3.5 CreateDrinksMenu
        //Create DrinksMenus
        DrinksMenu menu1 = new DrinksMenu
        {
            AvailableFrom = new DateTime(1, 1, 1, 15,30,00),
            Name = "Afternoon Drinks",
            ContainsAlcohol = true,
        };
        
        DrinksMenu menu2 = new DrinksMenu
        {
            AvailableFrom = new DateTime(1, 1, 1, 12,00,00),
            Name = "Noon Drinks",
            ContainsAlcohol = false,
        };
        
        DrinksMenu menu3 = new DrinksMenu
        {
            AvailableFrom = new DateTime(1, 1, 1, 19,30,00),
            Name = "Evening Drinks",
            ContainsAlcohol = true,
        };
        
        await dataAccess.CreateDrinksMenuAsync(menu1);
        await dataAccess.CreateDrinksMenuAsync(menu2);
        await dataAccess.CreateDrinksMenuAsync(menu3);
        
        //3.6 Add drinks to DrinksMenu

        Drink drink1 = new Drink
        {
            Name = "Afternoon Drink 1",
            Price = 12.95,
            AlcoholPercentage = 8.5,
            ContainsUmbrella = false
        };
        
        Drink drink2 = new Drink
        {
            Name = "Afternoon Drink 2",
            Price = 13.95,
            AlcoholPercentage = 10.5,
            ContainsUmbrella = true
        };
        
        Drink drink3 = new Drink
        {
            Name = "Noon Drink 1",
            Price = 4.95,
            AlcoholPercentage = 5.5,
            ContainsUmbrella = false
        };
        
        Drink drink4 = new Drink
        {
            Name = "Noon Drink 2",
            Price = 5.95,
            AlcoholPercentage = 6.5,
            ContainsUmbrella = false
        };
        
        Drink drink5 = new Drink
        {
            Name = "Evening Drink 1",
            Price = 7.95,
            AlcoholPercentage = 9.5,
            ContainsUmbrella = true
        };
        
        Drink drink6 = new Drink
        {
            Name = "Evening Drink 2",
            Price = 17.95,
            AlcoholPercentage = 19.5,
            ContainsUmbrella = true
        };

        await dataAccess.AddDrinkToMenuAsync(1, drink1);
        await dataAccess.AddDrinkToMenuAsync(1, drink2);
        await dataAccess.AddDrinkToMenuAsync(2, drink3);
        await dataAccess.AddDrinkToMenuAsync(2, drink4);
        await dataAccess.AddDrinkToMenuAsync(3, drink5);
        await dataAccess.AddDrinkToMenuAsync(3, drink6);
        
        //3.7 Get Drinks with filter

        List<Drink> filteredDrinks1 = await dataAccess.GetDrinksAsync(9.0, 20.0, true);
        foreach (Drink drink in filteredDrinks1)
        {
            Console.WriteLine(drink.Name);
            Console.WriteLine(drink.Price);
            Console.WriteLine(drink.AlcoholPercentage);
            Console.WriteLine(drink.ContainsUmbrella);
        }
        
        List<Drink> filteredDrinks2 = await dataAccess.GetDrinksAsync(3.5, 10.0, false);
        foreach (Drink drink in filteredDrinks2)
        {
            Console.WriteLine(drink.Name);
            Console.WriteLine(drink.Price);
            Console.WriteLine(drink.AlcoholPercentage);
            Console.WriteLine(drink.ContainsUmbrella);
        }
        
        //3.8 Get DrinksMenus ordered by price
        List<DrinksMenu> DrinksMenusOrderedByTotalPrice = await dataAccess.GetDrinksMenusOrderedByTotalPriceAsync();

        foreach (DrinksMenu drinksMenu in DrinksMenusOrderedByTotalPrice)
        {
            double sumOfDrinkPrices = 0.0;
            foreach (Drink drink in drinksMenu.Drinks)
            {
                sumOfDrinkPrices += drink.Price;
            }
            Console.WriteLine(drinksMenu.Name + ": " + sumOfDrinkPrices);
        }
    }
}