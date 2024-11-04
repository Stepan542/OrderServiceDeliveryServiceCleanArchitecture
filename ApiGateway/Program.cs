using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// работает только одна конфигурация
builder.Configuration.AddJsonFile("baseocelot.json");
builder.Configuration.AddJsonFile("orderocelot.json");
//builder.Configuration.AddJsonFile("deliveryocelot.json");

builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
// 1
//builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
// swagger не работает
app.UseSwaggerForOcelotUI();

await app.UseOcelot();
//app.UseHttpsRedirection();

app.Run();

