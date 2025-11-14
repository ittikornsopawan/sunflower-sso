using System;
using Infrastructure.Persistence.Entities;
using Domain.Interfaces.Repository;

using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Repository;

public class ParameterQueryRepository : BaseRepository, IParameterQueryRepository
{
    public ParameterQueryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<ParameterEntity>> GetParameters(string? key = null, string? category = null, string? language = null)
    {
        var query = _dbContext.m_parameters.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(key))
            query = query.Where(x => x.key == key);

        if (!string.IsNullOrEmpty(category))
            query = query.Where(x => x.category == category);

        if (!string.IsNullOrEmpty(language))
            query = query.Where(x => x.language == language);

        query = query.Where(p =>
            p.effectiveAt <= DateTime.UtcNow &&
            (p.expiresAt == null || p.expiresAt >= DateTime.UtcNow)
        );

        var entities = await query.ToListAsync();

        return entities.Select(e => new ParameterEntity(
            key: new ParameterKeyValueObject(e.key),
            effectivePeriod: new EffectivePeriodValueObject(e.effectiveAt, e.expiresAt),
            category: e.category != null ? new ParameterCategoryValueObject(e.category) : null,
            title: e.title != null ? new ParameterTitleValueObject(e.title) : null,
            description: e.description != null ? new ParameterDescriptionValueObject(e.description) : null,
            language: e.language != null ? new LanguageValueObject(e.language) : null,
            value: e.value != null ? new ParameterValue(e.value) : null,
            id: e.id
        )).ToList();

    }
}
