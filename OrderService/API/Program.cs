using Application.Mappers;
using Application.Services;
using Application.Interfaces;
using Infrastructure.Configurations;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Microsoft.Extensions.Options;
using Common.Infrastructure;
using Domain.Entities;
//using Common.Infrastructure;
//using MassTransit.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => {
    options.SuppressAsyncSuffixInActionNames = false;
});

// builder.Services.AddLogging(config => 
// {
//     config.AddConsole();
//     config.AddDebug();
// });

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) => 
    {
        var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

        cfg.Host(options.Host, h=>
        {
            h.Username(options.Username);
            h.Password(options.Password);
        });
    });
});

builder.Services.AddDbContext<OrderDbConext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrderDB")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile)); 
builder.Services.AddControllers();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped<BaseDbContext>(provider => provider.GetService<OrderDbConext>());
builder.Services.AddScoped<BaseDbContext>(provider => provider.GetService<OrderDbConext>()!);


builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

//app.UseHttpsRedirection();

app.Run();

