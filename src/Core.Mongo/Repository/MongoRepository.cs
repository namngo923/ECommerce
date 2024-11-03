using System.Linq.Expressions;
using Core.Mongo.Attributes;
using Core.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Shared.Exceptions;

namespace Core.Mongo.Repository;

public class MongoRepository<TDocument>(IMongoDatabase mongoDatabase) : IMongoRepository<TDocument>
    where TDocument : IBaseMongoEntity
{
    private readonly IMongoCollection<TDocument> _collection =
        mongoDatabase.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));

    public virtual IQueryable<TDocument> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).ToEnumerable();
    }

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression)
    {
        return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }

    public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).FirstOrDefault();
    }

    public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return await _collection.Find(filterExpression).FirstOrDefaultAsync();
    }

    public virtual TDocument FindById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.MongoObjectId, objectId);

        return _collection.Find(filter).SingleOrDefault();
    }

    public virtual async Task<TDocument> FindByIdAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.MongoObjectId, objectId);

        return await _collection.Find(filter).SingleOrDefaultAsync();
    }

    public virtual TDocument InsertOne(TDocument document)
    {
        _collection.InsertOne(document);

        return document;
    }

    public virtual async Task<TDocument> InsertOneAsync(TDocument document)
    {
        await _collection.InsertOneAsync(document);

        return document;
    }

    public void InsertMany(ICollection<TDocument> documents)
    {
        _collection.InsertMany(documents);
    }

    public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
    {
        await _collection.InsertManyAsync(documents);
    }

    public TDocument ReplaceOne(TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.MongoObjectId, document.MongoObjectId);

        return _collection.FindOneAndReplace(filter, document);
    }

    public virtual async Task<TDocument> ReplaceOneAsync(TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.MongoObjectId, document.MongoObjectId);

        return await _collection.FindOneAndReplaceAsync(filter, document);
    }

    public TDocument DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
    {
        return _collection.FindOneAndDelete(filterExpression);
    }

    public async Task<TDocument> DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return await _collection.FindOneAndDeleteAsync(filterExpression);
    }

    public TDocument DeleteById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.MongoObjectId, objectId);

        return _collection.FindOneAndDelete(filter);
    }

    public async Task<TDocument> DeleteByIdAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.MongoObjectId, objectId);

        return await _collection.FindOneAndDeleteAsync(filter);
    }

    public DeleteResult DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
    {
        return _collection.DeleteMany(filterExpression);
    }

    public async Task<DeleteResult> DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return await _collection.DeleteManyAsync(filterExpression);
    }

    private static string? GetCollectionName(Type documentType)
    {
        var atts = documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true);
        return atts.Length > 0
            ? ((BsonCollectionAttribute)atts.First()).CollectionName
            : throw new NotConfiguredMongoCollectionException(documentType.Name);
    }
}