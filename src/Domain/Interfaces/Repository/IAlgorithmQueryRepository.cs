using System;
using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IAlgorithmQueryRepository
{
    Task<List<AlgorithmEntity>> Get(List<string>? names = null);
}
