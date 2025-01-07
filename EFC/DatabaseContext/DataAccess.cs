using EFC.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFC.DatabaseContext;

public class DataAccess
{
    private readonly MenuContext DBContext;

    public DataAccess(MenuContext dbContext)
    {
        DBContext = dbContext;
    }

    public async Task<DrinksMenu> CreateDrinksMenuAsync(DrinksMenu menu)
    {
        //Method to count number of entries, primary keys will increment automatically though
        //menu.Id = await DBContext.Drinksmenus.CountAsync() + 1;
        
        //Retrieve the given DateTime, and convert it into only hours and minutes so that is the only part of the datetime that the menus can be compared on (Extra check)
        DateTime OldAvailableFrom = menu.AvailableFrom;
        menu.AvailableFrom = new DateTime(1, 1, 1, OldAvailableFrom.Hour, OldAvailableFrom.Minute,0);
        
        EntityEntry<DrinksMenu> entry = await DBContext.Drinksmenus.AddAsync(menu);
        await DBContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task AddDrinkToMenuAsync(int MenuId, Drink drink)
    {
        drink.DrinksMenuId = MenuId;
        await DBContext.Drinks.AddAsync(drink);
        await DBContext.SaveChangesAsync();
    }

    public async Task<List<Drink>> GetDrinksAsync(double? minAlcoholPercentage, double? maxAlcoholPercentage,
        bool? hasUmbrella)
    {
        IQueryable<Drink> query = DBContext.Drinks;

        if (minAlcoholPercentage.HasValue)
        {
            query = query.Where(d => d.AlcoholPercentage >= minAlcoholPercentage.Value);
        }

        if (maxAlcoholPercentage.HasValue)
        {
            query = query.Where(d => d.AlcoholPercentage <= maxAlcoholPercentage.Value);
        }

        if (hasUmbrella.HasValue)
        {
            query = query.Where(d => d.ContainsUmbrella == hasUmbrella.Value);
        }
        
        return await query.ToListAsync();
    }
    
    public async Task<List<DrinksMenu>> GetDrinksMenusOrderedByTotalPriceAsync()
    {
        // Retrieve all DrinksMenus and their related Drinks
        var drinksMenus = await DBContext.Drinksmenus
            .Include(menu => menu.Drinks) // Include the Drinks collection
            .ToListAsync();

        // Order the DrinksMenus by the total price of their Drinks in descending order
        return drinksMenus
            .OrderByDescending(menu => menu.Drinks.Sum(drink => drink.Price))
            .ToList();
    }
}