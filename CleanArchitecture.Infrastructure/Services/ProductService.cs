using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interface;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Services;

public class ProductService(EComDbContext context) : IProductService
{

    public async Task<IEnumerable<ProductDto>> GetProductsAsync(string searchTerm, decimal? minPrice, decimal? maxPrice,
        string sortBy, int pageNumber,
        int pageSize)
    {
        var query = context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
            query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice);

        query = sortBy switch
        {
            "price" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "name" => query.OrderBy(p => p.Name),
            "name_desc" => query.OrderByDescending(p => p.Name),
            _ => query.OrderBy(p => p.Name)
        };

        query = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return await query
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            })
            .ToListAsync();
    }
}
