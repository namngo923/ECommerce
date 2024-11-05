using Microsoft.EntityFrameworkCore;
using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Domain.Interface;

namespace ProductService.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> GetProductByIdAsync(string id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return product == null ? null : new ProductDto
        {
            Id = product.MongoObjectId.ToString(),
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        };
    }

    public async Task CreateProductAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            StockQuantity = productDto.StockQuantity
        };

        await _productRepository.AddProductAsync(product);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _productRepository.GetAllProductsAsync();

    public Task UpdateProductAsync(ProductDto productDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(string id)
    {
        throw new NotImplementedException();
    }
}