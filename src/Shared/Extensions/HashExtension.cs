using System;
using System.Security.Cryptography;
using System.Text;

namespace Shared.Extensions;

/// <summary>
/// Extension methods for hashing strings and byte arrays using MD5, SHA256, and SHA512.
/// </summary>
/// <author>Ittikorn Sopawan</author>
public static class HashExtension
{
    #region MD5

    /// <summary>
    /// Compute MD5 hash from a string (UTF8).
    /// </summary>
    /// <param name="text">Input string to hash.</param>
    /// <param name="asHex">If true, returns hash as hexadecimal string; otherwise, Base64 string.</param>
    /// <returns>MD5 hash as string in hex or Base64 format.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static string ToMd5(this string text, bool asHex = true)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Text cannot be empty", nameof(text));

        var bytes = Encoding.UTF8.GetBytes(text);
        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(bytes);
        return asHex ? Convert.ToHexString(hash).ToLower() : Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Compute MD5 hash from a byte array.
    /// </summary>
    /// <param name="data">Input bytes to hash.</param>
    /// <param name="asHex">If true, returns hash as hexadecimal string; otherwise, Base64 string.</param>
    /// <returns>MD5 hash as string in hex or Base64 format.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static string ToMd5(this byte[] data, bool asHex = true)
    {
        if (data == null || data.Length == 0)
            throw new ArgumentException("Data cannot be empty", nameof(data));

        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(data);
        return asHex ? Convert.ToHexString(hash).ToLower() : Convert.ToBase64String(hash);
    }

    #endregion

    #region SHA

    /// <summary>
    /// Compute SHA256 hash from a string (UTF8).
    /// </summary>
    /// <param name="text">Input string to hash.</param>
    /// <param name="asHex">If true, returns hash as hexadecimal string; otherwise, Base64 string.</param>
    /// <returns>SHA256 hash as string in hex or Base64 format.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static string ToSha256(this string text, bool asHex = true)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Text cannot be empty", nameof(text));

        var bytes = Encoding.UTF8.GetBytes(text);
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(bytes);
        return asHex ? Convert.ToHexString(hash).ToLower() : Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Compute SHA256 hash from a byte array.
    /// </summary>
    /// <param name="data">Input bytes to hash.</param>
    /// <param name="asHex">If true, returns hash as hexadecimal string; otherwise, Base64 string.</param>
    /// <returns>SHA256 hash as string in hex or Base64 format.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static string ToSha256(this byte[] data, bool asHex = true)
    {
        if (data == null || data.Length == 0)
            throw new ArgumentException("Data cannot be empty", nameof(data));

        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(data);
        return asHex ? Convert.ToHexString(hash).ToLower() : Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Compute SHA512 hash from a string (UTF8).
    /// </summary>
    /// <param name="text">Input string to hash.</param>
    /// <param name="asHex">If true, returns hash as hexadecimal string; otherwise, Base64 string.</param>
    /// <returns>SHA512 hash as string in hex or Base64 format.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static string ToSha512(this string text, bool asHex = true)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Text cannot be empty", nameof(text));

        var bytes = Encoding.UTF8.GetBytes(text);
        using var sha = SHA512.Create();
        var hash = sha.ComputeHash(bytes);
        return asHex ? Convert.ToHexString(hash).ToLower() : Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Compute SHA512 hash from a byte array.
    /// </summary>
    /// <param name="data">Input bytes to hash.</param>
    /// <param name="asHex">If true, returns hash as hexadecimal string; otherwise, Base64 string.</param>
    /// <returns>SHA512 hash as string in hex or Base64 format.</returns>
    /// <author>Ittikorn Sopawan</author>
    public static string ToSha512(this byte[] data, bool asHex = true)
    {
        if (data == null || data.Length == 0)
            throw new ArgumentException("Data cannot be empty", nameof(data));

        using var sha = SHA512.Create();
        var hash = sha.ComputeHash(data);
        return asHex ? Convert.ToHexString(hash).ToLower() : Convert.ToBase64String(hash);
    }

    #endregion
}