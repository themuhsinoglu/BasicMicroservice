using BasicMicroservice.Order.API.Consumers;
using BasicMicroservice.Order.API.Features.Orders;
using BasicMicroservice.Order.API.Repositories;
using BasicMicroservice.Shared;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());


builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<PaymentCompletedEventConsumer>();
    configurator.AddConsumer<StockNotReservedEventConsumer>();
    configurator.AddConsumer<PaymentFailedEventConsumer>();
    
    var rabbitMqHost = builder.Configuration.GetSection("MassTransit");
    configurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost["Host"]);
        
        cfg.ReceiveEndpoint(RabbitMqSettings.Order_PaymentCompletedEventQueue, e => e.ConfigureConsumer<PaymentCompletedEventConsumer>(context));
        
        cfg.ReceiveEndpoint(RabbitMqSettings.Order_StockNotReservedEventQueue, e => e.ConfigureConsumer<StockNotReservedEventConsumer>(context));
        
        cfg.ReceiveEndpoint(RabbitMqSettings.Order_PaymentFailedEventQueue, e => e.ConfigureConsumer<PaymentFailedEventConsumer>(context));
    });
});

var app = builder.Build();

app.AddOrderGroupEndpointExt();

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

app.MapGet("/", () => "Welcome OrderService");

app.Run();