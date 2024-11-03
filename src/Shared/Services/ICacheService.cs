namespace Shared.Services;

public interface ICacheService
{
    Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getItemCallback, TimeSpan expiration);
    Task<T> SetAsync<T>(string key, T item, TimeSpan expiration);
    Task DeleteAsync(string key);
}