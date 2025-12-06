using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

/// <summary>
/// value Object representing a Contact, which can be either an Email or a Mobile Number.
/// </summary>
/// <author>Ittikorn Sopawan</author>
public sealed record ContactValueObject
{
    /// <summary>
    /// The contact value (Email or Mobile Number).
    /// </summary>
    public string value { get; init; }

    /// <summary>
    /// The contact value (Email or Mobile Number) in Byte[].
    /// </summary>
    public byte[] valueByte { get; init; }

    /// <summary>
    /// Creates a new instance of <see cref="ContactValueObject"/> and validates the input.
    /// </summary>
    /// <param name="value">The contact value to create (must be a valid Email or Mobile Number).</param>
    /// <exception cref="ArgumentException">Thrown when the value is null, empty, or invalid.</exception>
    /// <returns>An instance of <see cref="ContactValueObject"/>.</returns>
    /// <author>Ittikorn Sopawan</author>
    public ContactValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Contact cannot be empty.", nameof(value));

        if (!IsValidEmail(value) && !IsValidMobile(value))
            throw new ArgumentException("Contact must be a valid email or mobile number.", nameof(value));

        this.value = value;
        this.valueByte = Encoding.UTF8.GetBytes(value);
    }

    /// <summary>
    /// Creates a new instance of <see cref="ContactValueObject"/> using a byte array.
    /// The byte array will be decoded to UTF-8 string and validated as either
    /// a valid email address or a valid mobile number.
    /// </summary>
    /// <param name="value">
    /// The contact value as a UTF-8 encoded byte array.
    /// Must represent a valid email or mobile number.
    /// </param>
    /// <returns>
    /// A validated and immutable <see cref="ContactValueObject"/> instance.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the byte array is null, empty, or its decoded string does not
    /// represent a valid email or mobile number.
    /// </exception>
    /// <author>Ittikorn Sopawan</author>
    public ContactValueObject(byte[] value)
    {
        if (value == null || value.Length == 0)
            throw new ArgumentException("Contact cannot be empty.", nameof(value));

        var asString = Encoding.UTF8.GetString(value);

        if (!IsValidEmail(asString) && !IsValidMobile(asString))
            throw new ArgumentException("Contact must be a valid email or mobile number.", nameof(value));

        this.value = asString;
        this.valueByte = value;
    }

    /// <summary>
    /// Validates whether the input is a properly formatted email.
    /// </summary>
    /// <param name="email">The email string to validate.</param>
    /// <returns>True if the input is a valid email; otherwise, false.</returns>
    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validates whether the input is a properly formatted mobile number.
    /// </summary>
    /// <param name="mobile">The mobile number string to validate.</param>
    /// <returns>True if the input is a valid mobile number; otherwise, false.</returns>
    private static bool IsValidMobile(string mobile)
    {
        // Assumes a mobile number with 9 to 15 digits
        var regex = new Regex(@"^\d{9,15}$");
        return regex.IsMatch(mobile);
    }

    public string Type()
    {
        if (IsValidEmail(this.value)) return "EMAIL";

        if (IsValidMobile(this.value)) return "MOBILE";

        return "UNDEFINED";
    }
}
