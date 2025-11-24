using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed record PasswordValueObject
{
    public string value { get; private set; }
    public string valueHash { get; private set; }
    public byte[] valueBytes { get; private set; }
    public bool isHashed { get; private set; }

    private static readonly Regex PasswordRegex = new(@"^[A-Za-z0-9_.]+$");

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

    public PasswordValueObject(byte[] value)
    {
        if (value == null || value.Length == 0)
            throw new ArgumentException("Value cannot be empty.", nameof(value));

        isHashed = true;
        valueBytes = value;
        valueHash = Convert.ToHexString(value).ToLower();
        this.value = valueHash;
    }

    public PasswordValueObject(string value, byte[] key, byte[] iv)
    {
        Validate(value);

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be empty.", nameof(value));
        if (key == null || iv == null)
            throw new ArgumentException("Key and IV cannot be null.");

        this.value = value;
        isHashed = true;

        // Encrypt with AES256
        var encrypted = AesEncrypt(Encoding.UTF8.GetBytes(value), key, iv);

        // MD5 hash
        valueBytes = Md5Hash(encrypted);
        valueHash = Convert.ToHexString(valueBytes).ToLower();
    }

    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password cannot be empty.");

        if (!PasswordRegex.IsMatch(value))
            throw new ArgumentException("Password must contain only English letters, numbers, '_' or '.'.");

        if (value.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters.");
    }

    public void EncryptWithKey(byte[] key, byte[] iv)
    {
        if (key == null || iv == null)
            throw new ArgumentException("Key and IV cannot be null.");

        // AES256 → MD5
        var encrypted = AesEncrypt(Encoding.UTF8.GetBytes(value), key, iv);
        valueBytes = Md5Hash(encrypted);
        valueHash = Convert.ToHexString(valueBytes).ToLower();
        isHashed = true;
    }

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

    private static byte[] Md5Hash(byte[] input)
    {
        using var md5 = MD5.Create();
        return md5.ComputeHash(input);
    }

    public override string ToString() => value;
}