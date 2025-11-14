using System;
using Domain.Entities;
using MediatR;

using Shared.Common;
using Shared.DTOs;

namespace Application.Parameter.Query;

public class ParameterQuery : IRequest<ResponseModel<List<ParameterEntity>?>>
{
    public string? key { get; }
    public string? category { get; }
    public string? language { get; }

    public ParameterQuery(string? key, string? category = null, string? language = null)
    {
        this.key = key;
        this.category = category;
        this.language = language;
    }
}
