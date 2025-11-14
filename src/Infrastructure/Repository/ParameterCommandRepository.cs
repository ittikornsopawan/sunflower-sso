using System;
using Infrastructure.Persistence.Entities;
using Domain.Interfaces.Repository;
using Domain.Entities;

namespace Infrastructure.Repository;

public class ParameterCommandRepository : IParameterCommandRepository
{
    public async Task<ParameterEntity> CreateParameter(ParameterEntity parameter)
    {
        throw new NotImplementedException();
    }

    public async Task<ParameterEntity> DeleteParameter(ParameterEntity parameter)
    {
        throw new NotImplementedException();
    }

    public async Task<ParameterEntity> UpdateParameter(ParameterEntity parameter)
    {
        throw new NotImplementedException();
    }
}
