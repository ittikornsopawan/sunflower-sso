using System;
using Domain.Interfaces.Repository;
using Infrastructure.Common;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class ParameterCommandRepository : BaseRepository, IParameterCommandRepository
{
    public ParameterCommandRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task Create(string key, string value, string? title = null, string? description = null, string? category = null, string? language = null)
    {
    }
}
