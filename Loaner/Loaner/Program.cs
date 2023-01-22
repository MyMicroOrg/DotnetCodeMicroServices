using Loaner.Configuration;
using Loaner.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var _services = builder.Services;
var _configuration = builder.Configuration;

_services.AddOptionSettings(_configuration);

var dataBaseOptions = _configuration.GetOptions<DatabaseSettingOptions>(DatabaseSettingOptions.SectionName);

_services.AddDbContext<LoanerDbContext>(options =>
{
    options.UseSqlServer(new SqlConnection(_configuration.DatabaseConnectionString()));
}, ServiceLifetime.Scoped);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
