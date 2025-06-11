using MongoDB.Bson.Serialization.Attributes;

namespace BasicMicroservice.Stock.API.Features.Stocks;

public class Stock
{
    [BsonElement("_id")]public Guid Id { get; set; }

    [BsonElement("_productId")]public string ProductId { get; set; } = default!;
    
    [BsonElement("_count")]public int Count { get; set; }
}