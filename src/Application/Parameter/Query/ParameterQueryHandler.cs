using System.Net;
using MediatR;

using Application.Common;
using Domain.Interfaces.Repository;
using Shared.Common;

namespace Application.Parameter.Query;

public class ParameterQueryHandler : CommonHandler, IRequestHandler<ParameterQuery, ResponseModel<List<string?>>>
{
    private readonly IParameterRepository _parameterRepository;

    public ParameterQueryHandler(IParameterRepository parameterRepository)
    {
        _parameterRepository = parameterRepository;
    }

    public async Task<ResponseModel<List<string?>>> Handle(ParameterQuery request, CancellationToken cancellationToken)
    {
        var parameters = await _parameterRepository.GetParameters(request.key!);
        if (parameters == null || !parameters.Any())
            return this.FailResponse<List<string?>>(HttpStatusCode.NotFound, "200001");

        var response = parameters.Select(x => x.value).ToList();

        return this.SuccessResponse(response);
    }
}