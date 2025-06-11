using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace BasicMicroservice.Stock.API.Respositories.Configurations;

public class StockEntityConfiguration: IEntityTypeConfiguration<Features.Stocks.Stock>
{
    public void Configure(EntityTypeBuilder<Features.Stocks.Stock> builder)
    {
        builder.ToCollection("stocks");
        builder.HasKey(x => x.Id);
    }
}