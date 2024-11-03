namespace SPSVN.Shared.Exceptions;

public class NotConfiguredMongoCollectionException(string collectionName)
    : Exception($"Collection {collectionName} is not configured.")
{
}