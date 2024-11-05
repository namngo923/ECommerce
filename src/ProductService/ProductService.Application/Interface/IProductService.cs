using ProductService.Application.DTOs;
using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> GetProductByIdAsync(string id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task CreateProductAsync(ProductDto productDto);
    Task UpdateProductAsync(ProductDto productDto);
    Task DeleteProductAsync(string id);
}