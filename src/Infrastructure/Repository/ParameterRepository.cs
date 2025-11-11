using System;
using Domain.Entities;
using Domain.Interfaces.Repository;

using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ParameterRepository : BaseRepository, IParameterRepository
{
    public ParameterRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<m_parameters>> GetParameters(string? key = null, string? category = null)
    {
        var query = _dbContext.m_parameters.AsQueryable();

        if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(category))
            return new List<m_parameters>();

        if (!string.IsNullOrEmpty(key))
            query = query.Where(p => p.key == key);

        if (!string.IsNullOrEmpty(category))
            query = query.Where(p => p.category == category);

        return await query.ToListAsync();
    }
}
