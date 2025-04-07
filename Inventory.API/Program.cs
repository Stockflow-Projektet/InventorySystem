using Inventory.API.RepositoryImplementations;
using Inventory.Core.Factories.Interfaces;
using Inventory.Core.RepositoryInterfaces;
using Inventory.Core.Services.Implementations;
using Inventory.Core.Services.Interfaces;
using Inventory.Logging;

LoggerConfigurator.ConfigureLogger("API");

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("Products") ?? "Data Source=Products.db";
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddDbContext<AppDbContext>(options =>
    //options.UseSqlServer(connectionString)); // or other database

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductFactory>();
builder.Services.AddScoped<IOrderFactory>();


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