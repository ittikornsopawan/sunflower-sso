using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class ParameterEntity : AggregateRoot
{
    public EffectivePeriodValueObject EffectivePeriod { get; private set; }
    public ParameterCategoryValueObject? Category { get; private set; }
    public ParameterKeyValueObject Key { get; private set; }
    public ParameterTitleValueObject? Title { get; private set; }
    public ParameterDescriptionValueObject? Description { get; private set; }
    public LanguageValueObject? Language { get; private set; }
    public ParameterValue? Value { get; private set; }

    public ParameterEntity(
        ParameterKeyValueObject key,
        EffectivePeriodValueObject effectivePeriod,
        ParameterCategoryValueObject? category = null,
        ParameterTitleValueObject? title = null,
        ParameterDescriptionValueObject? description = null,
        LanguageValueObject? language = null,
        ParameterValue? value = null,
        Guid? id = null
    )
    {
        this.id = id ?? Guid.NewGuid();
        Key = key;
        EffectivePeriod = effectivePeriod;
        Category = category;
        Title = title;
        Description = description;
        Language = language;
        Value = value;
    }
}
