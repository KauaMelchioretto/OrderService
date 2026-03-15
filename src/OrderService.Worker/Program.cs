using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Data;
using OrderService.Shared.Events;
using OrderService.Worker;
using OrderService.Worker.Consumers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=postgres;Port=5432;Database=ordersdb;Username=postgres;Password=postgres",
        o => o.EnableRetryOnFailure()));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("order-created-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
    });
});

var host = builder.Build();
host.Run();
