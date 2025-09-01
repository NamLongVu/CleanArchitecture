using CleanArchitecture.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CleanArchitecture.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    [EnableRateLimiting("fixed")]
    public async Task<IActionResult> GetProducts(
        string? searchTerm,
        decimal? minPrice,
        decimal? maxPrice,
        string? sortBy = "name",
        int pageNumber = 1,
        int pageSize = 10)
    {
        var products = await productService.GetProductsAsync(
            searchTerm, minPrice, maxPrice, sortBy, pageNumber, pageSize);

        return Ok(products);
    }
}