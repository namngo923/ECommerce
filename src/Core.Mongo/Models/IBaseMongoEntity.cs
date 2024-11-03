using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Mongo.Models;

public interface IBaseMongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    ObjectId MongoObjectId { get; set; }
}