using Microsoft.EntityFrameworkCore;

namespace BasicMicroservice.Stock.API.Respositories;

public static class SeedData
{
    public static async Task AddSeedDataExt(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<StockDbContext>();
        
        context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

        if (!await context.Stocks.AnyAsync())
        {
            await context.Stocks.AddRangeAsync(new List<Features.Stocks.Stock>
            {
                new() { Id = Guid.NewGuid(), Count = 100, ProductId = "1234" },
                new() { Id = Guid.NewGuid(), Count = 50, ProductId = "4567" },
                new() { Id = Guid.NewGuid(), Count = 200, ProductId = "7890" }
            });

            await context.SaveChangesAsync();
        }
    }
}