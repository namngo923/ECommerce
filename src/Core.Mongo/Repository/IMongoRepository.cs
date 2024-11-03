using System.Linq.Expressions;
using Core.Mongo.Models;
using MongoDB.Driver;

namespace Core.Mongo.Repository;

public interface IMongoRepository<TDocument> where TDocument : IBaseMongoEntity
{
    IQueryable<TDocument> AsQueryable();

    IEnumerable<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression);

    IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression);

    TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

    Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

    TDocument FindById(string id);

    Task<TDocument> FindByIdAsync(string id);

    TDocument InsertOne(TDocument document);

    Task<TDocument> InsertOneAsync(TDocument document);

    void InsertMany(ICollection<TDocument> documents);

    Task InsertManyAsync(ICollection<TDocument> documents);

    TDocument ReplaceOne(TDocument document);

    Task<TDocument> ReplaceOneAsync(TDocument document);

    TDocument DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

    Task<TDocument> DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

    TDocument DeleteById(string id);

    Task<TDocument> DeleteByIdAsync(string id);

    DeleteResult DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

    Task<DeleteResult> DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
}