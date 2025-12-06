using System;

namespace Shared.Configurations;


public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = new();
    public RedisOptions Redis { get; set; } = new();
    public EncryptionOptions Encryption { get; set; } = new();
    public EncryptionOptions DbEncryption { get; set; } = new();
    public EmailSettings EmailSettings { get; set; } = new();
    public TokenOptions Token { get; set; } = new();
    public PaginationOptions Pagination { get; set; } = new();
    public HealthCheck HealthCheck { get; set; } = new();
    public KeyValue KeyValue { get; set; } = new();
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; } = default!;
}

public class RedisOptions
{
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public string Password { get; set; } = default!;
}

public class EncryptionOptions
{
    public string Key { get; set; } = default!;
    public string IV { get; set; } = default!;
}

public class TokenOptions
{
    public int ExpireBufferMinutes { get; set; } = 15;
    public int AccessTokenExpirationMinutes { get; set; } = 60;
    public int RefreshTokenExpirationDays { get; set; } = 30;
}

public class PaginationOptions
{
    public int DefaultPageSize { get; set; } = 20;
}

public class HealthCheck
{
    public string Database { get; set; } = default!;
}

public class EmailSettings
{
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string From { get; set; } = default!;
    public int RetryAttempt { get; set; } = 3;
    public int RetryDelay { get; set; } = 500;
}

public class KeyValue
{
    public string OTPEmailTemplate { get; set; } = default!;
}
