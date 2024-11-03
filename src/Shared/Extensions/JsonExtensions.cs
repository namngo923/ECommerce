using System.Text.Encodings.Web;
using System.Text.Json;

//using Newtonsoft.Json;

namespace Shared.Extensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public static T? Deserialize<T>(this string json)
    {
        return string.IsNullOrWhiteSpace(json) ? default : JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);

        //return string.IsNullOrWhiteSpace(json) ? default : JsonConvert.DeserializeObject<T>(json);
    }

    public static string Serialize(this object obj)
    {
        return obj == null ? string.Empty : JsonSerializer.Serialize(obj, JsonSerializerOptions);
        //return obj == null ? string.Empty : JsonConvert.SerializeObject(obj);
    }
}