using Inventory.Core;
using Inventory.Core.Database;
using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Factories.Implementations;
using Inventory.Core.RepositoryInterfaces;
using Inventory.Core.Repositories;
using Inventory.Core.RepositoriesImplementations;
using Inventory.Core.Services.Interfaces;
using Inventory.Core.Services.Implementations;
using Inventory.Logging;  // <-- your custom logging
using Microsoft.AspNetCore.Mvc;
using Serilog;

LoggerConfigurator.ConfigureLogger("Inventory.API");

var builder = WebApplication.CreateBuilder(args);

// 1) Configure Serilog logging
LoggerConfigurator.ConfigureLogger("Inventory.API");

// 2) Check for the connection string in appsettings.json
string? connectionString = builder.Configuration.GetConnectionString("InventoryLocalConnection");
if (string.IsNullOrEmpty(connectionString))
{
    Log.Fatal("No connection string 'InventoryLocalConnection' found in appsettings.");
    throw new InvalidOperationException("Cannot start API without a valid connection string.");
}

// 3) Initialize the DB connection (Singleton)
DatabaseConnection.Initialize(connectionString);

// 4) Register your Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IDepotRepository, DepotRepository>();
builder.Services.AddScoped<ITransferRepository, TransferRepository>();

// 5) Register your Factories
builder.Services.AddSingleton<IProductFactoryResolverService, ProductFactoryResolverService>();
builder.Services.AddSingleton<IOrderFactory, OrderFactory>();

// 6) Register your Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
// The InventoryService code below will need to be properly implemented
// to call _inventoryRepo.GetTotalQuantityForProductAsync(...) etc.
builder.Services.AddScoped<IInventoryService, InventoryService>();

// 7) Add minimal API + controllers (if any)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 8) Middleware pipeline
app.UseHttpsRedirection();
app.UseAuthorization();

// If you have controllers, you can do:
// app.MapControllers();

// Otherwise, or in addition, define minimal API routes:

// ----- Products -----
app.MapGet("/api/products", async (IProductService productService) =>
    await productService.GetProducts()
);

app.MapGet("/api/products/{id}", async (IProductService productService, int id) =>
    await productService.GetProductById(id)
);

app.MapGet("/api/products/search/{query}", async (IProductService productService, string query) =>
    await productService.QueryProducts(query)
);

app.MapPost("/api/products", async ([FromBody] ProductCreationArgs args, IProductService productService) =>
{
    await productService.AddProduct(args);
    return Results.Created("/api/products", args);
});

app.MapPut("/api/products/{id}", async (int id, [FromBody] ProductCreationArgs args, IProductService productService) =>
{
    await productService.UpdateProduct(id, args);
    return Results.NoContent();
});

app.MapDelete("/api/products/{id}", async (int id, IProductService productService) =>
{
    await productService.DeleteProduct(id);
    return Results.NoContent();
});

// ----- Orders -----
// Note: make sure to finalize your IOrderService methods like GetOrders(), etc.

app.MapGet("/api/orders", async (IOrderService orderService) =>
    await orderService.GetOrders()
);

app.MapGet("/api/orders/{orderId}", async (int orderId, IOrderService orderService) =>
    await orderService.GetOrderById(orderId)
);

// Simplistic "create order" example
app.MapPost("/api/orders", async ([FromBody] int detailId, IOrderService orderService) =>
{
    await orderService.PlaceOrder(detailId);
    return Results.Ok();
});

app.MapDelete("/api/orders/{orderId}", async (int orderId, IOrderService orderService) =>
{
    await orderService.DeleteOrder(orderId);
    return Results.NoContent();
});

if (app.Environment.IsDevelopment())
{
    // swagger for quick testing
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
