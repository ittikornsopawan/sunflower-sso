using System;

namespace Domain.ValueObjects;

public enum TypeContext
{
    Otp,
    Notification,
    Contact
}

public sealed record TypeValueObject
{
    public string value { get; init; }
    public TypeContext Context { get; init; }

    private static readonly Dictionary<TypeContext, HashSet<string>> AllowedMap =
        new()
        {
            {
                TypeContext.Otp,
                new HashSet<string> { "LOGIN", "VERIFY", "RESET_PASSWORD", "CONFIRM", "OTHER" }
            },
            {
                TypeContext.Notification,
                new HashSet<string> { "EMAIL", "SMS", "PUSH" }
            },
            {
                TypeContext.Contact,
                new HashSet<string> { "EMAIL", "MOBILE" }
            }
        };

    public TypeValueObject(TypeContext context, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Type value cannot be empty.", nameof(value));

        value = value.Trim().ToUpper();

        if (!AllowedMap.TryGetValue(context, out var allowedSet))
            throw new InvalidOperationException($"Unknown TypeContext: {context}");

        if (!allowedSet.Contains(value))
            throw new InvalidOperationException(
                $"Invalid type '{value}' for context '{context}'. Allowed: {string.Join(", ", allowedSet)}");

        this.value = value;
        Context = context;
    }
}
