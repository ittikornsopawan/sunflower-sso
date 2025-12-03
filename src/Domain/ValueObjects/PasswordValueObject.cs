using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

/// <summary>
/// Represents a secure password value object that supports:
/// - Plain password validation
/// - MD5 hashing
/// - AES256 encryption + MD5 hashing
/// - Verification with or without encryption
/// </summary>
/// <remarks>
/// Author: Ittikorn Sopawan
/// </remarks>
public sealed record PasswordValueObject
{
    /// <summary>Original value (plain password or hashed)</summary>
    public string value { get; private set; }

    /// <summary>Hashed representation (lowercase hex)</summary>
    public string valueHash { get; private set; }

    /// <summary>Hashed bytes of the password</summary>
    public byte[] valueBytes { get; private set; }

    /// <summary>Indicates whether the stored value was already hashed</summary>
    public bool isHashed { get; private set; }

    private static readonly Regex PasswordRegex = new(@"^[A-Za-z0-9_.]+$");

    /// <summary>
    /// Creates a new password value object.
    /// If isHashed = false → MD5 hash of password.
    /// If isHashed = true → treat input string as already hashed.
    /// </summary>
    /// <param name="value">Plain or hashed password text.</param>
    /// <param name="isHashed">Indicates if the value is already hashed.</param>
    /// <exception cref="ArgumentException">Thrown when password format is invalid.</exception>
    public PasswordValueObject(string value, bool isHashed = false)
    {
        Validate(value);

        this.value = value;
        this.isHashed = isHashed;

        if (isHashed)
        {
            valueHash = value;
            valueBytes = Encoding.UTF8.GetBytes(value);
        }
        else
        {
            valueBytes = Md5Hash(Encoding.UTF8.GetBytes(value));
            valueHash = Convert.ToHexString(valueBytes).ToLower();
        }
    }

    /// <summary>
    /// Creates a password object directly from hashed bytes.
    /// </summary>
    /// <param name="value">Hashed bytes</param>
    /// <exception cref="ArgumentException"></exception>
    public PasswordValueObject(byte[] value)
    {
        if (value == null || value.Length == 0)
            throw new ArgumentException("Value cannot be empty.", nameof(value));

        isHashed = true;
        valueBytes = value;
        valueHash = Convert.ToHexString(value).ToLower();
        this.value = valueHash;
    }

    /// <summary>
    /// Encrypts password with AES256 (byte[] key/iv) then MD5 hashes the encrypted result.
    /// </summary>
    /// <param name="value">Plain password</param>
    /// <param name="key">AES encryption key (256-bit)</param>
    /// <param name="iv">AES IV (128-bit)</param>
    public PasswordValueObject(string value, byte[] key, byte[] iv)
    {
        Validate(value);

        if (key == null || iv == null)
            throw new ArgumentException("Key and IV cannot be null.");

        this.value = value;
        isHashed = true;

        var encrypted = AesEncrypt(Encoding.UTF8.GetBytes(value), key, iv);

        valueBytes = Md5Hash(encrypted);
        valueHash = Convert.ToHexString(valueBytes).ToLower();
    }

    /// <summary>
    /// Encrypts password with AES256 using string key/iv.
    /// Supports UTF8 text keys.
    /// </summary>
    /// <param name="value">Plain password</param>
    /// <param name="key">Key string (UTF8)</param>
    /// <param name="iv">IV string (UTF8)</param>
    public PasswordValueObject(string value, string key, string iv)
    {
        Validate(value);

        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(iv))
            throw new ArgumentException("Key and IV cannot be null or empty.");

        this.value = value;
        isHashed = true;

        var keyBytes = Encoding.UTF8.GetBytes(key);
        var ivBytes = Encoding.UTF8.GetBytes(iv);

        var encrypted = AesEncrypt(Encoding.UTF8.GetBytes(value), keyBytes, ivBytes);

        valueBytes = Md5Hash(encrypted);
        valueHash = Convert.ToHexString(valueBytes).ToLower();
    }

    /// <summary>
    /// Validates password format rules.
    /// </summary>
    /// <param name="value">Password to validate</param>
    /// <exception cref="ArgumentException"></exception>
    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password cannot be empty.");

        if (!PasswordRegex.IsMatch(value))
            throw new ArgumentException("Password must contain only English letters, numbers, '_' or '.'.");

        if (value.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters.");
    }

    /// <summary>
    /// Encrypts existing value using AES256 + MD5.
    /// </summary>
    /// <param name="key">AES key</param>
    /// <param name="iv">AES IV</param>
    public void EncryptWithKey(byte[] key, byte[] iv)
    {
        if (key == null || iv == null)
            throw new ArgumentException("Key and IV cannot be null.");

        var encrypted = AesEncrypt(Encoding.UTF8.GetBytes(value), key, iv);
        valueBytes = Md5Hash(encrypted);
        valueHash = Convert.ToHexString(valueBytes).ToLower();
        isHashed = true;
    }

    /// <summary>
    /// Verifies provided password against stored hash.
    /// </summary>
    /// <param name="plainPassword">Password to verify</param>
    /// <param name="key">Optional AES key</param>
    /// <param name="iv">Optional AES IV</param>
    /// <returns>True if match, false otherwise</returns>
    public bool Verify(string plainPassword, byte[]? key = null, byte[]? iv = null)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
            return false;

        byte[] hashBytes;

        if (key != null && iv != null)
        {
            var encrypted = AesEncrypt(Encoding.UTF8.GetBytes(plainPassword), key, iv);
            hashBytes = Md5Hash(encrypted);
        }
        else
        {
            hashBytes = Md5Hash(Encoding.UTF8.GetBytes(plainPassword));
        }

        var hashString = Convert.ToHexString(hashBytes).ToLower();
        return hashString == valueHash;
    }

    /// <summary>
    /// Performs AES256 CBC encryption.
    /// </summary>
    private static byte[] AesEncrypt(byte[] plainBytes, byte[] key, byte[] iv)
    {
        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
    }

    /// <summary>
    /// Computes MD5 hash of input bytes.
    /// </summary>
    private static byte[] Md5Hash(byte[] input)
    {
        using var md5 = MD5.Create();
        return md5.ComputeHash(input);
    }

    /// <summary>
    /// Returns the original value (not the hash)
    /// </summary>
    public override string ToString() => value;
}