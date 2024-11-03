using Core.Mongo.Configuration;
using Core.Mongo.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Core.Mongo;

public static class DependencyInjection
{
    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDbSettings = new MongoDbSettings();
        configuration.GetSection(MongoDbSettings.SectionName).Bind(mongoDbSettings);

        services.AddSingleton(mongoDbSettings);

        services.AddSingleton<IMongoClient>(sp =>
        {
            var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);

            return mongoClient;
        });

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            var database = mongoClient.GetDatabase(mongoDbSettings.DatabaseName);

            return database;
        });

        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

        return services;
    }
}