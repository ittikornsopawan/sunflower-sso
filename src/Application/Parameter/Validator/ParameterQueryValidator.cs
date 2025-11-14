using System;
using Application.Parameter.Query;
using FluentValidation;

namespace Application.Parameter.Validator;

public class ParameterQueryValidator : AbstractValidator<ParameterQuery>
{
    public ParameterQueryValidator()
    {
        RuleFor(x => x.key)
            .MaximumLength(128)
            .WithMessage("Parameter 'key' must not exceed 128 characters.");

        RuleFor(x => x.category)
            .MaximumLength(128)
            .WithMessage("Parameter 'category' must not exceed 128 characters.");
    }
}
