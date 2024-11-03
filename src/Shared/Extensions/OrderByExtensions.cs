using System.Text.Json;

namespace Shared.Extensions;

public static class OrderByExtensions
{
    public static IEnumerable<Dictionary<string, object?>> OrderByDynamicField(
        this IEnumerable<Dictionary<string, object?>> data, string fieldName, bool asc = true)
    {
        if (!data.Any()) return data;

        var firstItem = data.First()[fieldName];
        var dataType = firstItem == null ? typeof(string) : firstItem.GetType();
        if (dataType == typeof(JsonElement))
        {
            if (firstItem == null)
            {
                dataType = typeof(string);
            }
            else
            {
                var typeOfObject = ((JsonElement)firstItem).ValueKind;
                dataType = typeOfObject switch
                {
                    JsonValueKind.Number => typeof(double),
                    JsonValueKind.True or JsonValueKind.False => typeof(bool),
                    _ => typeof(string)
                };
            }
        }

        if (dataType == typeof(int) || dataType == typeof(int?))
            data = asc
                ? data.OrderBy(x => Convert.ToInt32($"{x[fieldName]}"))
                : data.OrderByDescending(x => Convert.ToInt32($"{x[fieldName]}"));
        else if (dataType == typeof(long) || dataType == typeof(long?))
            data = asc
                ? data.OrderBy(x => Convert.ToInt64($"{x[fieldName]}"))
                : data.OrderByDescending(x => Convert.ToInt64($"{x[fieldName]}"));
        else if (dataType == typeof(double) || dataType == typeof(double?))
            data = asc
                ? data.OrderBy(x => Convert.ToInt64($"{x[fieldName]}"))
                : data.OrderByDescending(x => Convert.ToInt64($"{x[fieldName]}"));
        else if (dataType == typeof(bool) || dataType == typeof(bool?))
            data = asc
                ? data.OrderBy(x => Convert.ToBoolean($"{x[fieldName]}"))
                : data.OrderByDescending(x => Convert.ToBoolean($"{x[fieldName]}"));
        else if (dataType == typeof(DateTime) || dataType == typeof(DateTime?))
            data = asc
                ? data.OrderBy(x => Convert.ToDateTime($"{x[fieldName]}"))
                : data.OrderByDescending(x => Convert.ToDateTime($"{x[fieldName]}"));
        else if (dataType == typeof(decimal) || dataType == typeof(decimal?))
            data = asc
                ? data.OrderBy(x => Convert.ToDecimal($"{x[fieldName]}"))
                : data.OrderByDescending(x => Convert.ToDecimal($"{x[fieldName]}"));
        else
            data = asc
                ? data.OrderBy(x => Convert.ToString($"{x[fieldName]}"))
                : data.OrderByDescending(x => Convert.ToString($"{x[fieldName]}"));

        return data;
    }
}