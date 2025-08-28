using CleanArchitecture.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(
        string? searchTerm,
        decimal? minPrice,
        decimal? maxPrice,
        string? sortBy = "name",
        int pageNumber = 1,
        int pageSize = 10)
    {
        var products = await _productService.GetProductsAsync(
            searchTerm, minPrice, maxPrice, sortBy, pageNumber, pageSize);

        return Ok(products);
    }
}