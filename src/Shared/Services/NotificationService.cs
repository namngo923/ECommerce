using System.Text;

namespace Shared.Services;

public interface INotificationService
{
    Task SendNotificationAsync(string endpoint, string message);
}

public class NotificationService(IHttpClientFactory httpClientFactory) : INotificationService
{
    public async Task SendNotificationAsync(string endpoint, string message)
    {
        try
        {
            var client = httpClientFactory.CreateClient();
            var content = new StringContent(message, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endpoint, content);

            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}