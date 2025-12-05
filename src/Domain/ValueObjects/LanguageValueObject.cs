using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed record LanguageValueObject
{
    /// <summary>
    /// Language code (e.g., EN, TH, JP). Defaults to TH if null.
    /// </summary>
    public string value { get; init; }

    private static readonly Regex LanguageRegex = new(@"^[A-Z]{2,4}$");

    /// <summary>
    /// Creates a new LanguageValueObject. Null means default TH.
    /// </summary>
    /// <param name="value">Language code</param>
    /// <exception cref="ArgumentException">Thrown when language format is invalid</exception>
    public LanguageValueObject(string? value)
    {
        // Default to TH if null
        value = value?.Trim().ToUpper() ?? "TH";

        if (!LanguageRegex.IsMatch(value))
            throw new ArgumentException("Language must be 2–4 uppercase letters (e.g., EN, TH, JP).", nameof(value));

        this.value = value;
    }

    /// <summary>
    /// Returns true if language value is default TH.
    /// </summary>
    public bool IsDefault => value == "TH";
}
