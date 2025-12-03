using System;
using System.Text;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents an algorithm record in the domain layer.
/// Maps from table "key".m_algorithms
/// </summary>
/// <remarks>
/// Author: Ittikorn Sopawan
/// </remarks>
public class AlgorithmEntity : AggregateRoot
{
    public NameValueObject name { get; private set; }
    private byte[] algorithm { get; set; }
    public EffectivePeriodValueObject period { get; private set; }
    public object? keyRequired { get; private set; }

    /// <summary>
    /// Returns the algorithm as a UTF-8 string.
    /// </summary>
    public string value => Encoding.UTF8.GetString(algorithm);

    public AlgorithmEntity(
        NameValueObject name,
        byte[] algorithm,
        EffectivePeriodValueObject period,
        object? keyRequired = null,
        Guid? id = null
    )
    {
        this.id = id ?? Guid.NewGuid();
        this.name = name ?? throw new ArgumentNullException(nameof(name));
        this.algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
        this.period = period ?? throw new ArgumentNullException(nameof(period));
        this.keyRequired = keyRequired;
    }

    /// <summary>
    /// Checks if the algorithm is currently effective.
    /// </summary>
    public bool IsEffective() =>
        DateTime.UtcNow >= period.effectiveAt &&
        (period.expiresAt == null || DateTime.UtcNow <= period.expiresAt);

    /// <summary>
    /// Updates the algorithm with new byte[] and refreshes the string value.
    /// </summary>
    public void UpdateAlgorithm(byte[] newAlgorithm)
    {
        if (newAlgorithm == null || newAlgorithm.Length == 0)
            throw new ArgumentException("Algorithm cannot be null or empty.", nameof(newAlgorithm));

        algorithm = newAlgorithm;
    }
}