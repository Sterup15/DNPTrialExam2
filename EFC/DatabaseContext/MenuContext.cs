using EFC.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace EFC.DatabaseContext;

public class MenuContext : DbContext
{
    public DbSet<DrinksMenu> Drinksmenus => Set<DrinksMenu>();
    public DbSet<Drink> Drinks => Set<Drink>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // In real applications, the database connection string would be stored in a configuration file, but for the purpose of simplicity, this approach is used:
        optionsBuilder.UseSqlite("Data Source=G:\\My Drive\\Softwareingeniør - VIA\\3. Semester\\DNP1\\DNPTrialExam23AReExam\\EFC\\database.db");
    }
    
}