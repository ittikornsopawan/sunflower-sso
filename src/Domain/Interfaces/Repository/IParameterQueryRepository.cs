using System;
using Shared.DTOs;

namespace Domain.Interfaces.Repository;

public interface IParameterQueryRepository
{
    Task<List<ParameterDTO>?> GetParameters(string? key = null, string? category = null);
}
