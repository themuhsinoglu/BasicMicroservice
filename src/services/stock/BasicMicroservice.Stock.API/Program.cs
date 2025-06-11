using BasicMicroservice.Shared;
using BasicMicroservice.Stock.API.Consumers;
using BasicMicroservice.Stock.API.Options;
using BasicMicroservice.Stock.API.Respositories;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<OrderCreatedEventConsumer>();
    configurator.AddConsumer<PaymentFailedEventConsumer>();
    
    var rabbitMqHost = builder.Configuration.GetSection("MassTransit");
    
    configurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost["Host"]);
        
        cfg.ReceiveEndpoint(RabbitMqSettings.Stock_OrderCreatedEventQueue, e => e.ConfigureConsumer<OrderCreatedEventConsumer>(context));
        
        cfg.ReceiveEndpoint(RabbitMqSettings.Stock_PaymentFailedEventQueue, e => e.ConfigureConsumer<PaymentFailedEventConsumer>(context));
    });
});

var app = builder.Build();

app.AddSeedDataExt()?.ContinueWith(x =>
{
    Console.WriteLine(x.IsFaulted ? x.Exception?.Message : "Seed Data has been saved successfully.");
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Welcome StockService");

app.Run();