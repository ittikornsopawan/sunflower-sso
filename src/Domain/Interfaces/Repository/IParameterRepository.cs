using System;
using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IParameterRepository
{
    Task<List<m_parameters>> GetParameters(string? key = null, string? category = null);
}
