using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Shared.Extensions;

public static class VariableExtension
{
    public static Dictionary<string, string> Parse(string variablesJson)
    {
        if (string.IsNullOrWhiteSpace(variablesJson))
            throw new ArgumentException("Template variables JSON is empty.", nameof(variablesJson));

        Dictionary<string, string>? variables;
        try
        {
            variables = JsonSerializer.Deserialize<Dictionary<string, string>>(variablesJson.Replace('\'', '\"'));
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Invalid JSON for template variables.", nameof(variablesJson), ex);
        }

        if (variables == null || variables.Count == 0)
            throw new ArgumentException("Template variables dictionary is empty.");

        return variables;
    }

    public static string Replace(this string template, Dictionary<string, string>? variables)
    {
        if (string.IsNullOrEmpty(template) || variables == null || variables.Count == 0)
            return template;

        return Regex.Replace(template, @"\{\{(\w+)\}\}", match =>
        {
            var key = match.Groups[1].Value;
            if (variables.TryGetValue(key, out var value))
                return value ?? string.Empty;

            return match.Value;
        });
    }
}
