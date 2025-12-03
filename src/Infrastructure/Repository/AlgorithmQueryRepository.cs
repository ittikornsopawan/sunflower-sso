using System;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.ValueObjects;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class AlgorithmQueryRepository : BaseRepository, IAlgorithmQueryRepository
{
    public AlgorithmQueryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// Gets all algorithms with optional name filter.
    /// Only returns algorithms within effective period.
    /// </summary>
    /// <param name="names">Optional list of algorithm names to filter</param>
    /// <returns>List of AlgorithmEntity</returns>
    public async Task<List<AlgorithmEntity>> Get(List<string>? names = null)
    {
        var query = _dbContext.m_algorithms.AsNoTracking().AsQueryable();

        if (names != null && names.Any()) query = query.Where(a => names.Contains(a.name));
        query = query.Where(a => a.effectiveAt <= DateTime.UtcNow && (a.expiresAt == null || a.expiresAt >= DateTime.UtcNow));

        var entities = await query.ToListAsync();
        return entities.Select(e => new AlgorithmEntity(
            name: new NameValueObject(e.name),
            algorithm: e.algorithm,
            period: new EffectivePeriodValueObject(e.effectiveAt, e.expiresAt),
            keyRequired: e.keyRequired,
            id: e.id
        )).ToList();
    }
}
