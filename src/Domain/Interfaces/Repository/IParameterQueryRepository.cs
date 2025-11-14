using System;
using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IParameterQueryRepository
{
    Task<List<ParameterEntity>> GetParameters(string? key = null, string? category = null, string? language = null);
}
