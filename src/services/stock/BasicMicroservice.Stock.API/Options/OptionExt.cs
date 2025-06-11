using Microsoft.Extensions.Options;

namespace BasicMicroservice.Stock.API.Options;

public static class OptionExt
{
    public static IServiceCollection AddOptionsExt(this IServiceCollection services)
    {
        services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations()
            .ValidateOnStart();


        services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOption>>().Value);


        return services;
    }
}