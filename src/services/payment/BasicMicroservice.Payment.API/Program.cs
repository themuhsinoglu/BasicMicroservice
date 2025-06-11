using BasicMicroservice.Payment.API.Consumers;
using BasicMicroservice.Shared;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<StockReservedEventConsumer>();
    
    var rabbitMqHost = builder.Configuration.GetSection("MassTransit");
    configurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost["Host"]);
        
        cfg.ReceiveEndpoint(RabbitMqSettings.Payment_StockReservedEventQueue, e => e.ConfigureConsumer<StockReservedEventConsumer>(context));
    });
});

var app = builder.Build();

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

app.MapGet("/", () => "Welcome PaymentService");

app.Run();