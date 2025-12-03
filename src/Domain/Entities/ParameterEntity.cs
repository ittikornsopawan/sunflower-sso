using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class ParameterEntity : AggregateRoot
{
    public EffectivePeriodValueObject period { get; private set; }
    public ParameterCategoryValueObject? category { get; private set; }
    public ParameterKeyValueObject key { get; private set; }
    public ParameterTitleValueObject? title { get; private set; }
    public ParameterDescriptionValueObject? description { get; private set; }
    public LanguageValueObject? language { get; private set; }
    public ParameterValue? value { get; private set; }

    public ParameterEntity(
        ParameterKeyValueObject key,
        EffectivePeriodValueObject period,
        ParameterCategoryValueObject? category = null,
        ParameterTitleValueObject? title = null,
        ParameterDescriptionValueObject? description = null,
        LanguageValueObject? language = null,
        ParameterValue? value = null,
        Guid? id = null
    )
    {
        this.id = id ?? Guid.NewGuid();
        this.key = key;
        this.period = period;
        this.category = category;
        this.title = title;
        this.description = description;
        this.language = language;
        this.value = value;
    }
}
