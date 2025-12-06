using System;
using Domain.Interfaces.Repository;

namespace Domain.UseCases.Parameter.Services;

public interface IParameterService
{
}

public class ParameterService : IParameterService
{
    private readonly IParameterQueryRepository _parameterQueryRepository;
    private readonly IParameterCommandRepository _parameterCommandRepository;
    public ParameterService(IParameterQueryRepository parameterQueryRepository, IParameterCommandRepository parameterCommandRepository)
    {
        _parameterQueryRepository = parameterQueryRepository;
        _parameterCommandRepository = parameterCommandRepository;
    }
}
