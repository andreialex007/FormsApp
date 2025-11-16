using System.Text.Json;

namespace FormsApp.Core.Common;

public static class JsonValidator
{
    public static bool IsValidJson(string? content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return false;

        try
        {
            using var doc = JsonDocument.Parse(content);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
