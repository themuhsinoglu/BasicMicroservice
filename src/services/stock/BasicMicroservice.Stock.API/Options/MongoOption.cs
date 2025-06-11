using System.ComponentModel.DataAnnotations;

namespace BasicMicroservice.Stock.API.Options;

public class MongoOption
{
    [Required] public string DatabaseName { get; set; } = default!;
    [Required] public string ConnectionString { get; set; } = default!;
}