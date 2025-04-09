using Inventory.API.RepositoryImplementations;
using Inventory.Core.Factories.Interfaces;
using Inventory.Core.RepositoryInterfaces;
using Inventory.Core.Services.Implementations;
using Inventory.Core.Services.Interfaces;
using Inventory.Logging;
using Inventory.Core.Database;
using Serilog;

LoggerConfigurator.ConfigureLogger("API");

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

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


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IProductFactoryResolverService, ProductFactoryResolverService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();
// Map product endpoints
app.MapPost("/product", (IProductService productService, string productDto) => productService.AddProduct(productDto)).RequireAuthorization();
app.MapGet("/products", (IProductService productService) => productService.GetProducts());
app.MapGet("/product/{id}", (IProductService productService, int id) => productService.GetProductById(id));
app.MapGet("/product/search/{query}", (IProductService productService, string query) => productService.QueryProducts(query));
app.MapPut("/product/{id}", (IProductService productService, int id) => productService.UpdateProduct(id)).RequireAuthorization();
app.MapDelete("/product/{id}", (IProductService productService, int id) => productService.DeleteProduct(id)).RequireAuthorization();

// Map order endpoints
app.MapPost("/order", (IOrderService orderService, string orderDetail) => orderService.PlaceOrder(orderDetail)).RequireAuthorization();
app.MapGet("/orders", (IOrderService orderService) => orderService.GetOrders());
app.MapGet("/order/{orderid}", (IOrderService orderService, int orderId) => orderService.GetOrderById(orderId));
app.MapGet("/order/search/{query}", (IOrderService orderService, string query) => orderService.QueryOrders(query));
app.MapPut("/order/{orderid}", (IOrderService orderService, int orderId) => orderService.UpdateOrder(orderId)).RequireAuthorization();
app.MapDelete("/order/{orderid}", (IOrderService orderService, int orderId) => orderService.DeleteOrder(orderId)).RequireAuthorization();

app.Run();