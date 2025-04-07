using Serilog;
using Inventory.Logging;
using Inventory.Core.Database;

LoggerConfigurator.ConfigureLogger("API");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Serilog logging right away
LoggerConfigurator.ConfigureLogger("Inventory.API");

// Get your connection string from appsettings.json
string? connectionString = builder.Configuration.GetConnectionString("InventoryLocalConnection");
if (string.IsNullOrEmpty(connectionString))
{
    // If not found or empty, log a fatal error (Serilog is configured!)
    Log.Fatal("No connection string 'InventoryLocalConnection' found in appsettings.");
    throw new InvalidOperationException("Cannot start API without a valid connection string.");
}

// Initialize the singleton so Inventory.Core can create DB connections
DatabaseConnection.Initialize(connectionString);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
