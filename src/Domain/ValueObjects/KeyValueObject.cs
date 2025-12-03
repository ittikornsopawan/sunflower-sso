using System.Text;

namespace Domain.ValueObjects;

/// <summary>
/// Represents a key value in both string and byte[] formats.
/// This value object ensures strong typing and consistent transformation
/// between text-based representation and binary representation.
///
/// Commonly used for cryptographic keys, encoded algorithm values,
/// or any domain field where both raw bytes and string formats are required.
/// </summary>
/// <remarks>
/// Author: Ittikorn Sopawan
/// </remarks>
public sealed record KeyValueObject
{
    /// <summary>
    /// The UTF-8 decoded string representation of the key value.
    /// </summary>
    public string value { get; private set; }

    /// <summary>
    /// The raw byte[] representation of the key value.
    /// </summary>
    public byte[] valueByte { get; private set; }

    /// <summary>
    /// Creates a KeyValueObject using a string input.
    /// Automatically converts the string into a UTF-8 byte[] representation.
    /// </summary>
    /// <param name="value">The textual key value.</param>
    /// <exception cref="ArgumentException">Thrown when value is null or empty.</exception>
    public KeyValueObject(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Key value cannot be null or empty.", nameof(value));

        this.value = value.Trim();
        this.valueByte = Encoding.UTF8.GetBytes(this.value);
    }

    /// <summary>
    /// Creates a KeyValueObject using a byte[] input.
    /// Automatically converts the byte[] into a UTF-8 string representation.
    /// </summary>
    /// <param name="value">The raw byte array representing the key value.</param>
    /// <exception cref="ArgumentException">Thrown when byte[] is null or empty.</exception>
    public KeyValueObject(byte[] value)
    {
        if (value == null || value.Length == 0)
            throw new ArgumentException("Key byte array cannot be null or empty.", nameof(value));

        this.valueByte = value;
        this.value = Encoding.UTF8.GetString(value);
    }

    /// <summary>
    /// Returns the textual representation of the key.
    /// </summary>
    public override string ToString() => value;

    /// <summary>
    /// Returns a copy of the key value in byte[] format.
    /// Ensures immutability by not exposing internal array directly.
    /// </summary>
    /// <returns>A new byte[] containing the key value.</returns>
    public byte[] ToBytes() => (byte[])valueByte.Clone();
}
