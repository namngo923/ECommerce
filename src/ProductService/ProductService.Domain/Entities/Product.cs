using Core.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductService.Domain.Entities;

public class Product : IBaseMongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public ObjectId MongoObjectId { get; set ; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public double Price { get; set; }
    public string Category { get; set; } = default!;
    public int StockQuantity { get; set; }
}