using System.Globalization;

namespace Domain.ValueObjects;

public record LanguageValueObject
{
    public string value { get; init; }

    public LanguageValueObject(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            try
            {
                _ = CultureInfo.GetCultureInfo(value);
            }
            catch
            {
                throw new ArgumentException("Invalid language code", nameof(value));
            }
        }

        this.value = value;
    }

    public override string ToString() => this.value;
}
