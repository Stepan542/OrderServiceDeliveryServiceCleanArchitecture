using Application.Services;
using Domain.Interfaces;
using Infrastructure.Configurations;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Application.Consumers;
using Microsoft.Extensions.Options;
using Application.Mappers;
using Common.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddDbContext<DeliveryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DeliveryDb")));

builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryService, DeliveryOrderService>();
builder.Services.AddScoped<BaseDbContext>(provider => provider.GetService<DeliveryDbContext>()!);

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderForDeliveryConsumer>(config => 
    {
        config.UseMessageRetry(retryConfig => 
        {
            retryConfig.Interval(3, TimeSpan.FromSeconds(5));
        });
    });

    x.UsingRabbitMq((context, cfg) => 
    {
        var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

        cfg.Host(options.Host, h =>
        {
            h.Username(options.Username);
            h.Password(options.Password);
        });


        cfg.ReceiveEndpoint("order-delivery-clean-arch-next", e =>
        {
            e.Consumer<OrderForDeliveryConsumer>(context);
        });
    });
}
);

// ?
builder.Services.AddSwaggerGen();
// ?
builder.Services.AddAutoMapper(typeof(DeliveryMappingProfile));

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

//app.UseHttpsRedirection();






