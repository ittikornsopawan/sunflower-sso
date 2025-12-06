using System;
using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IParameterCommandRepository
{
    Task Create(ParameterEntity parameter);
}
