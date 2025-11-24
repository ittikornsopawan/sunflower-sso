using System.Text;
using System.Text.Json;
using Shared.Extensions;

namespace Domain.ValueObjects;

/// <summary>
/// ValueObject representing algorithm configuration for password encryption/hashing.
/// Contains AlgorithmId and actual cryptographic parameters like Key, IV, Salt.
/// </summary>
/// <remarks>
/// Author: Ittikorn Sopawan
/// </remarks>
public sealed record AlgorithmValueObject
{
    /// <summary>
    /// Algorithm type identifier (corresponds to m_algorithms.id)
    /// </summary>
    public Guid algorithmId { get; init; }

    /// <summary>
    /// Salt for hashing / KDF algorithms (Base64)
    /// </summary>
    private string? salt { get; init; }

    /// <summary>
    /// Encryption key (Base64)
    /// </summary>
    private string? key { get; init; }

    /// <summary>
    /// Initialization vector (Base64)
    /// </summary>
    private string? iv { get; init; }

    /// <summary>
    /// Encryption key as byte array
    /// </summary>
    public byte[]? keyBytes { get; init; }

    /// <summary>
    /// Initialization vector as byte array
    /// </summary>
    public byte[]? ivBytes { get; init; }

    /// <summary>
    /// Optional algorithm value string (can contain placeholders)
    /// </summary>
    public string? value { get; init; }

    /// <summary>
    /// Constructor for AlgorithmValueObject
    /// </summary>
    /// <param name="algorithmId">Algorithm ID (UUID)</param>
    /// <param name="value">Optional string template</param>
    /// <param name="key">Encryption key in Base64</param>
    /// <param name="iv">Initialization vector in Base64</param>
    /// <param name="salt">Salt in Base64</param>
    public AlgorithmValueObject(Guid algorithmId, string value, string key, string iv, string salt)
    {
        if (algorithmId == Guid.Empty)
            throw new ArgumentException("AlgorithxmId cannot be empty GUID.", nameof(algorithmId));

        this.algorithmId = algorithmId;
        this.value = value;
        this.key = key;
        this.iv = iv;
        this.salt = salt;

        this.keyBytes = Encoding.UTF8.GetBytes(this.key);
        this.ivBytes = Encoding.UTF8.GetBytes(this.iv);
    }

    /// <summary>
    /// Generates input string for encryption or hashing based on AlgorithmValueObject template.
    /// Replaces placeholders: {salt}, {key}, {iv}, {plainText}.
    /// </summary>
    /// <param name="plainText">Plaintext or data</param>
    /// <returns>Prepared string ready for encryption/hash</returns>
    /// <author>Ittikorn Sopawan</author>
    public string GenerateAlgorithmInput(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
            throw new ArgumentException("PlainText cannot be null or empty.", nameof(plainText));

        string template = this.value ?? "{plainText}";

        if (!string.IsNullOrEmpty(this.salt))
            template = template.Replace("{salt}", this.salt);

        if (!string.IsNullOrEmpty(this.key))
            template = template.Replace("{key}", this.key);

        if (!string.IsNullOrEmpty(this.iv))
            template = template.Replace("{iv}", this.iv);

        template = template.Replace("{plainText}", plainText);

        if (string.IsNullOrEmpty(this.key) || string.IsNullOrEmpty(this.iv))
            throw new InvalidOperationException("Key and IV must be set for encryption.");

        return EncryptionExtension.EncryptAesToBase64(template, this.keyBytes!, this.ivBytes!);
    }
}