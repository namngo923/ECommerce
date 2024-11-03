namespace Core.Mongo.Configuration;

public class MongoDbSettings
{
    public const string SectionName = "MongoDbSettings";

    public string DatabaseName { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
}