using Core.Mongo.Configuration;
using Core.Mongo.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ProductService.Domain.Entities;
using ProductService.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoRepository<Product> _products;

        public ProductRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _products = (IMongoRepository<Product>?)database.GetCollection<Product>("Products");
        }

        public async Task<Product> GetProductByIdAsync(string id) =>
            await _products.FindByIdAsync(id);

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
             _products.FilterBy(d => !string.IsNullOrEmpty(d.Name));

        public async Task AddProductAsync(Product product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateProductAsync(Product product) =>
            await _products.ReplaceOneAsync(product);

        public async Task DeleteProductAsync(string id) =>
            await _products.DeleteByIdAsync(id);
    }


}
