using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Inventory.Frontend.Services.Implementations;
using Serilog;
using Inventory.Logging;

LoggerConfigurator.ConfigureLogger("Frontend");

var builder = WebApplication.CreateBuilder(args);

// Read config for UseMockServices
bool useMock = builder.Configuration.GetValue<bool>("UseMockServices");
string apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");


// Register services conditionally
if (useMock)
{
    // Register mock services
    builder.Services.AddScoped<IProductService, ProductMockService>();
    builder.Services.AddScoped<IOrderService, OrderMockService>();
}
else
{
    // If real API, set up HttpClient
    builder.Services.AddHttpClient<IProductService, ProductService>(client =>
    {
        client.BaseAddress = new Uri(apiBaseUrl);
    });
    builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
    {
        client.BaseAddress = new Uri(apiBaseUrl);
    });
}

// Add services to the container.
builder.Services.AddRazorPages();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
