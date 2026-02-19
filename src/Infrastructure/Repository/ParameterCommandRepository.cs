using System;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Common;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class ParameterCommandRepository : BaseRepository, IParameterCommandRepository
{
    public ParameterCommandRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

}
