using Microsoft.EntityFrameworkCore;
using PlateformService.AsyncDataServices;
using PlateformService.SyncDataService.Http;
using PlatefromService.Data;
using PlatefromService.Models;
using PlaterformService.Data;

var builder = WebApplication.CreateBuilder(args);

// if (!builder.Environment.IsProduction())
// {
    System.Console.WriteLine("------- IN Mem DB -------");
    builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseInMemoryDatabase("InMem"));
// }
// else
// {
//     System.Console.WriteLine("------- IN Sql Server -------");
//     builder.Services.AddDbContext<AppDbContext>(opt =>
//     opt.UseSqlServer(builder.Configuration.GetConnectionString("PlateformsConn")));
// }

builder.Services.AddScoped<IPlateformRepo, PlateformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine($"---- CommandService Endpoint {builder.Configuration["Commandservice"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepData.PrepPopulation(app, builder.Environment.IsProduction());
app.Run();
