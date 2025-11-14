using System;
using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IParameterCommandRepository
{
    Task<ParameterEntity> CreateParameter(ParameterEntity parameter);
    Task<ParameterEntity> UpdateParameter(ParameterEntity parameter);
    Task<ParameterEntity> DeleteParameter(ParameterEntity parameter);
}
