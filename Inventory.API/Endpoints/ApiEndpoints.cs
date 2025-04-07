using Inventory.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Endpoints;

public class ApiEndpointsMapper
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;

    public ApiEndpointsMapper(IOrderService orderService, IProductService productService)
    {
        _productService = productService;
        _orderService = orderService;
    }

    public void MapProductEndpoints(WebApplication app)
    {
        app.MapGet("/Product", (IProductService productService) => productService.GetProducts());
        app.MapGet("/Product/{id}", (IProductService productService, int id) => productService.GetProductById(id));
        app.MapGet("/Product/query/{query}",
            (IProductService productService, string query) => productService.QueryProducts(query));
        app.MapPost("/Product", (IProductService productService) => productService.AddProductAsync())
            .RequireAuthorization();
        app.MapPut("/Product/{id}", (IProductService productService, int id) => productService.UpdateProduct(id))
            .RequireAuthorization();
        app.MapDelete("/Product/{id}", (IProductService productService, int id) => productService.DeleteProduct(id))
            .RequireAuthorization();
    }

    public void MapOrderEndpoints(WebApplication app)
    {
        app.MapPost("/order",
                (IOrderService orderService, [FromBody] string orderDetail) => orderService.PlaceOrder(orderDetail))
            .RequireAuthorization();
        app.MapGet("/orders", (IOrderService orderService) => orderService.GetOrders());
        app.MapGet("/order/{id}", (IOrderService orderService, int orderId) => orderService.GetOrderById(orderId));
        app.MapGet("/order/query/{query}",
            (IOrderService orderService, string query) => orderService.QueryOrders(query));
        app.MapPut("/order/{id}", (IOrderService orderService, int orderId) => orderService.UpdateOrder(orderDto))
            .RequireAuthorization();
        app.MapDelete("/order/{id}", (IOrderService orderService, int orderId) => orderService.DeleteOrder(orderId))
            .RequireAuthorization();
    }
}