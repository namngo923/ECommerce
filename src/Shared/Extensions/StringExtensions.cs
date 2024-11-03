using System.Security;
using System.Text;

namespace SPSVN.Shared.Extensions;

public static class StringExtensions
{
    public static bool IsGuid(this string input)
    {
        return Guid.TryParse(input, out _);
    }

    public static bool IsNonEmptyGuid(this string input)
    {
        var result = Guid.TryParse(input, out var outVal);

        return result && outVal != default;
    }

    public static Guid ToGuid(this string input)
    {
        return !input.IsGuid() ? throw new ArgumentException("The given string must be parsable to a Guid.") : Guid.Parse(input);
    }

    public static bool IsEmpty(this string input)
    {
        return string.IsNullOrWhiteSpace(input);
    }

    public static SecureString ToSecureString(this string input)
    {
        var key = input.ToCharArray();
        var sc = new SecureString();
        foreach (var c in key) sc.AppendChar(c);

        return sc;
    }

    public static bool ToBoolean(this string input)
    {
        var trimmed = input.Trim().ToUpper();

        return trimmed == "TRUE" || trimmed == "1" || trimmed == "YES";
    }

    public static string RemoveSpecialCharacters(this string str)
    {
        var sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}
