using CleanArchitecture.Application.DTOs;

namespace CleanArchitecture.Application.Interface;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync(
        string searchTerm, 
        decimal? minPrice, 
        decimal? maxPrice,
        string sortBy,
        int pageNumber,
        int pageSize);
}