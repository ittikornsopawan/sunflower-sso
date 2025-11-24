using System;
using System.Security.Cryptography;
using System.Text;

namespace Shared.Extensions;

public static class EncryptionExtension
{
    // ================= BYTE[] KEY/IV =================

    /// <summary>
    /// Encrypt plain bytes using AES-256 with byte[] key/iv.
    /// </summary>
    /// <param name="plainBytes">The plaintext bytes to encrypt.</param>
    /// <param name="key">The encryption key (must be 32 bytes for AES-256).</param>
    /// <param name="iv">The initialization vector (must be 16 bytes).</param>
    /// <param name="mode">The cipher mode (default is CBC).</param>
    /// <param name="padding">The padding mode (default is PKCS7).</param>
    /// <returns>Encrypted bytes.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static byte[] EncryptAes(this byte[] plainBytes, byte[] key, byte[] iv, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        if (plainBytes == null || plainBytes.Length == 0) throw new ArgumentException("Plain bytes cannot be empty");
        if (key == null || key.Length != 32) throw new ArgumentException("Key must be 32 bytes (AES-256)");
        if (iv == null || iv.Length != 16) throw new ArgumentException("IV must be 16 bytes");

        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Mode = mode;
        aes.Padding = padding;
        aes.Key = key;
        aes.IV = iv;

        using var encryptor = aes.CreateEncryptor();
        return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
    }

    /// <summary>
    /// Decrypt cipher bytes using AES-256 with byte[] key/iv.
    /// </summary>
    /// <param name="cipherBytes">The encrypted bytes to decrypt.</param>
    /// <param name="key">The encryption key (must be 32 bytes for AES-256).</param>
    /// <param name="iv">The initialization vector (must be 16 bytes).</param>
    /// <param name="mode">The cipher mode (default is CBC).</param>
    /// <param name="padding">The padding mode (default is PKCS7).</param>
    /// <returns>Decrypted bytes.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static byte[] DecryptAes(this byte[] cipherBytes, byte[] key, byte[] iv, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        if (cipherBytes == null || cipherBytes.Length == 0) throw new ArgumentException("Cipher bytes cannot be empty");
        if (key == null || key.Length != 32) throw new ArgumentException("Key must be 32 bytes (AES-256)");
        if (iv == null || iv.Length != 16) throw new ArgumentException("IV must be 16 bytes");

        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Mode = mode;
        aes.Padding = padding;
        aes.Key = key;
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        return decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
    }

    /// <summary>
    /// Encrypt a string to AES-256 using byte[] key/iv and return Base64.
    /// </summary>
    /// <param name="plainText">The plaintext string to encrypt.</param>
    /// <param name="key">The encryption key (must be 32 bytes for AES-256).</param>
    /// <param name="iv">The initialization vector (must be 16 bytes).</param>
    /// <param name="mode">The cipher mode (default is CBC).</param>
    /// <param name="padding">The padding mode (default is PKCS7).</param>
    /// <returns>The encrypted data as a Base64 string.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static string EncryptAesToBase64(this string plainText, byte[] key, byte[] iv, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        var encrypted = bytes.EncryptAes(key, iv, mode, padding);
        return Convert.ToBase64String(encrypted);
    }

    /// <summary>
    /// Decrypt a Base64 AES string using byte[] key/iv.
    /// </summary>
    /// <param name="base64Cipher">The Base64-encoded ciphertext to decrypt.</param>
    /// <param name="key">The encryption key (must be 32 bytes for AES-256).</param>
    /// <param name="iv">The initialization vector (must be 16 bytes).</param>
    /// <param name="mode">The cipher mode (default is CBC).</param>
    /// <param name="padding">The padding mode (default is PKCS7).</param>
    /// <returns>The decrypted plaintext string.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static string DecryptAesFromBase64(this string base64Cipher, byte[] key, byte[] iv, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        var cipherBytes = Convert.FromBase64String(base64Cipher);
        var decrypted = cipherBytes.DecryptAes(key, iv, mode, padding);
        return Encoding.UTF8.GetString(decrypted);
    }

    // ================= STRING KEY/IV =================

    /// <summary>
    /// Encrypt plain bytes using AES-256 with string key/iv (UTF8), return byte[].
    /// </summary>
    /// <param name="plainBytes">The plaintext bytes to encrypt.</param>
    /// <param name="keyStr">The encryption key string (must produce 32 bytes in UTF8).</param>
    /// <param name="ivStr">The initialization vector string (must produce 16 bytes in UTF8).</param>
    /// <param name="mode">The cipher mode (default is CBC).</param>
    /// <param name="padding">The padding mode (default is PKCS7).</param>
    /// <returns>Encrypted bytes.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static byte[] EncryptAes(this byte[] plainBytes, string keyStr, string ivStr, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        if (plainBytes == null || plainBytes.Length == 0) throw new ArgumentException("Plain bytes cannot be empty");
        if (string.IsNullOrEmpty(keyStr)) throw new ArgumentException("Key cannot be empty");
        if (string.IsNullOrEmpty(ivStr)) throw new ArgumentException("IV cannot be empty");

        var key = Encoding.UTF8.GetBytes(keyStr);
        var iv = Encoding.UTF8.GetBytes(ivStr);

        if (key.Length != 32) throw new ArgumentException("Key must be 32 bytes (AES-256)");
        if (iv.Length != 16) throw new ArgumentException("IV must be 16 bytes");

        return plainBytes.EncryptAes(key, iv, mode, padding);
    }

    /// <summary>
    /// Decrypt cipher bytes using AES-256 with string key/iv (UTF8).
    /// </summary>
    /// <param name="cipherBytes">The encrypted bytes to decrypt.</param>
    /// <param name="keyStr">The encryption key string (must produce 32 bytes in UTF8).</param>
    /// <param name="ivStr">The initialization vector string (must produce 16 bytes in UTF8).</param>
    /// <param name="mode">The cipher mode (default is CBC).</param>
    /// <param name="padding">The padding mode (default is PKCS7).</param>
    /// <returns>Decrypted bytes.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static byte[] DecryptAes(this byte[] cipherBytes, string keyStr, string ivStr, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        if (cipherBytes == null || cipherBytes.Length == 0) throw new ArgumentException("Cipher bytes cannot be empty");
        if (string.IsNullOrEmpty(keyStr)) throw new ArgumentException("Key cannot be empty");
        if (string.IsNullOrEmpty(ivStr)) throw new ArgumentException("IV cannot be empty");

        var key = Encoding.UTF8.GetBytes(keyStr);
        var iv = Encoding.UTF8.GetBytes(ivStr);

        if (key.Length != 32) throw new ArgumentException("Key must be 32 bytes (AES-256)");
        if (iv.Length != 16) throw new ArgumentException("IV must be 16 bytes");

        return cipherBytes.DecryptAes(key, iv, mode, padding);
    }

    /// <summary>
    /// Encrypt a string to AES-256 using string key/iv and return Base64.
    /// </summary>
    /// <param name="plainText">The plaintext string to encrypt.</param>
    /// <param name="keyStr">The encryption key string (must produce 32 bytes in UTF8).</param>
    /// <param name="ivStr">The initialization vector string (must produce 16 bytes in UTF8).</param>
    /// <param name="mode">The cipher mode (default is CBC).</param>
    /// <param name="padding">The padding mode (default is PKCS7).</param>
    /// <returns>The encrypted data as a Base64 string.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static string EncryptAesToBase64(this string plainText, string keyStr, string ivStr, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        var encrypted = bytes.EncryptAes(keyStr, ivStr, mode, padding);
        return Convert.ToBase64String(encrypted);
    }

    /// <summary>
    /// Decrypt a Base64 AES string using string key/iv.
    /// </summary>
    /// <param name="base64Cipher">The Base64-encoded ciphertext to decrypt.</param>
    /// <param name="keyStr">The encryption key string (must produce 32 bytes in UTF8).</param>
    /// <param name="ivStr">The initialization vector string (must produce 16 bytes in UTF8).</param>
    /// <param name="mode">The cipher mode (default is CBC).</param>
    /// <param name="padding">The padding mode (default is PKCS7).</param>
    /// <returns>The decrypted plaintext string.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static string DecryptAesFromBase64(this string base64Cipher, string keyStr, string ivStr, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        var cipherBytes = Convert.FromBase64String(base64Cipher);
        var decrypted = cipherBytes.DecryptAes(keyStr, ivStr, mode, padding);
        return Encoding.UTF8.GetString(decrypted);
    }
}