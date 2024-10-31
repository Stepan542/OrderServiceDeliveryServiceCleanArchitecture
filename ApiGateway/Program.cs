using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
// 1
//builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwaggerForOcelotUI();

await app.UseOcelot();
//app.UseHttpsRedirection();

app.Run();

