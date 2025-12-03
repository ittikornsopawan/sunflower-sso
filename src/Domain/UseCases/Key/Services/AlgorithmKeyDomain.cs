using System;
using System.Reflection.Metadata;
using Domain.Entities;
using Domain.Interfaces.Repository;

namespace Domain.UseCases.Key.Services;

/// <summary>
/// Domain service for retrieving algorithms based on parameters or name filter.
/// </summary>
/// <remarks>
/// Author: Ittikorn Sopawan
/// </remarks>
public interface IAlgorithmKeyDomain
{
    /// <summary>
    /// Retrieves algorithm entities filtered by name(s).
    /// If no name is provided, uses the 'ALGORITHM_AUTHORIZATION' parameter values.
    /// </summary>
    /// <param name="names">Optional list of algorithm names to filter.</param>
    /// <returns>List of AlgorithmEntity</returns>
    Task<List<AlgorithmEntity>> Get(List<string>? names = null);
}

public class AlgorithmKeyDomain : IAlgorithmKeyDomain
{
    private readonly IParameterQueryRepository _parameterQueryRepository;
    private readonly IAlgorithmQueryRepository _algorithmQueryRepository;

    public AlgorithmKeyDomain(IParameterQueryRepository parameterQueryRepository, IAlgorithmQueryRepository algorithmQueryRepository)
    {
        _parameterQueryRepository = parameterQueryRepository ?? throw new ArgumentNullException(nameof(parameterQueryRepository));
        _algorithmQueryRepository = algorithmQueryRepository ?? throw new ArgumentNullException(nameof(algorithmQueryRepository));
    }

    public async Task<List<AlgorithmEntity>> Get(List<string>? names = null)
    {
        var algorithms = await _algorithmQueryRepository.Get(names);

        return algorithms ?? throw new InvalidOperationException("No algorithms found for the provided names.");
    }
}