using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents a configurable parameter entity used for storing 
/// categorized, localized, and versionable system parameters.
/// </summary>
public class ParameterEntity : BaseEntity
{
    /// <summary>
    /// The effective period (start–end) during which this parameter is valid.
    /// </summary>
    public EffectivePeriodValueObject period { get; private set; }

    /// <summary>
    /// The category this parameter belongs to (optional).
    /// Used to group related parameters.
    /// </summary>
    public CategoryValueObject category { get; private set; }

    /// <summary>
    /// The unique key identifier of this parameter.
    /// </summary>
    public KeyValueObject key { get; private set; }

    /// <summary>
    /// The title or display name of the parameter (optional).
    /// </summary>
    public TitleValueObject title { get; private set; }

    /// <summary>
    /// A detailed explanation of the parameter (optional).
    /// </summary>
    public DescriptionValueObject description { get; private set; }

    /// <summary>
    /// The language code (e.g., EN, TH, JP) associated with the parameter.
    /// </summary>
    public LanguageValueObject language { get; private set; }

    /// <summary>
    /// The actual parameter value stored as a value object.
    /// </summary>
    public ValueValueObject value { get; private set; }

    /// <summary>
    /// Creates and initializes a new parameter entity.
    /// </summary>
    /// <param name="period">Valid effective period.</param>
    /// <param name="category">Category of the parameter (optional).</param>
    /// <param name="key">Unique parameter key (required).</param>
    /// <param name="title">Display title (optional).</param>
    /// <param name="description">Description (optional).</param>
    /// <param name="language">Language of the value (required).</param>
    /// <param name="value">Actual stored parameter value (required).</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when required value objects (key, language, value) are null.
    /// </exception>
    public ParameterEntity(EffectivePeriodValueObject period, CategoryValueObject category, KeyValueObject key, TitleValueObject title, DescriptionValueObject description, LanguageValueObject language, ValueValueObject value)
    {
        this.id = Guid.NewGuid();
        this.period = period;
        this.category = category;
        this.key = key ?? throw new ArgumentNullException(nameof(key));
        this.title = title;
        this.description = description;
        this.language = language ?? throw new ArgumentNullException(nameof(language));
        this.value = value ?? throw new ArgumentNullException(nameof(value));
    }
}
