using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BasicMicroservice.Stock.API.Respositories;

public class StockDbContext(DbContextOptions<StockDbContext> options) : DbContext(options)
{
    public DbSet<Features.Stocks.Stock> Stocks { get; set; }
    
    public static StockDbContext Create(IMongoDatabase database)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<StockDbContext>().UseMongoDB(database.Client,
                database.DatabaseNamespace.DatabaseName);

        return new StockDbContext(optionsBuilder.Options);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}