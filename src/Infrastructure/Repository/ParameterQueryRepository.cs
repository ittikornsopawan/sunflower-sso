using System;
using Domain.Interfaces.Repository;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

namespace Infrastructure.Repository;

public class ParameterQueryRepository : BaseRepository, IParameterQueryRepository
{
    public ParameterQueryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<ParameterDTO>?> GetParameters(string? key = null, string? category = null)
    {
        var query = this._dbContext.m_parameters.AsQueryable();

        if (!string.IsNullOrEmpty(key)) query = query.Where(p => p.key == key);

        if (!string.IsNullOrEmpty(category)) query = query.Where(p => p.category == category);

        var result = await query.ToListAsync();
        if (result == null) return null;

        return result.Select(p => new ParameterDTO
        {
            category = p.category,
            key = p.key,
            title = p.title,
            description = p.description,
            language = p.language,
            value = p.value
        }).ToList();
    }
}
