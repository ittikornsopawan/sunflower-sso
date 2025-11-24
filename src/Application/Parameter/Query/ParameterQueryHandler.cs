using System.Net;
using MediatR;

using Application.Common;
using Domain.Interfaces.Repository;
using Shared.Common;
using Shared.DTOs;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Application.Parameter.Query;

public class ParameterQueryHandler : CommonHandler, IRequestHandler<ParameterQuery, ResponseModel<List<ParameterEntity>?>>
{
    private readonly IParameterQueryRepository _parameterQueryRepository;
    public ParameterQueryHandler(AppDbContext dbContext, IParameterQueryRepository parameterQueryRepository) : base(dbContext)
    {
        _parameterQueryRepository = parameterQueryRepository;
    }

    public async Task<ResponseModel<List<ParameterEntity>?>> Handle(ParameterQuery request, CancellationToken cancellationToken)
    {
        var response = await _parameterQueryRepository.GetParameters(request.key, request.category, request.language);
        if (response == null || !response.Any())
            return this.FailResponse<List<ParameterEntity>?>(HttpStatusCode.NotFound, "200001");

        return this.SuccessResponse<List<ParameterEntity>?>(response);
    }
}