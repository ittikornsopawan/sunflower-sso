using System;

using MediatR;

using Shared.Common;

namespace Application.Parameter.Query;

public class ParameterQuery : IRequest<ResponseModel<List<string?>>>
{
    public string? key { get; }
    public string? category { get; }

    public ParameterQuery(string? key, string? category = null)
    {
        this.key = key;
        this.category = category;
    }
}
