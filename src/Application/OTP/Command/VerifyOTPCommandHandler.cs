using System;
using Application.Common;
using Infrastructure.Persistence;
using MediatR;
using Shared.Common;

namespace Application.Otp.Command;

public class VerifyOTPCommandHandler : CommonHandler, IRequestHandler<VerifyOTPCommand, ResponseModel<Guid>>
{
    public VerifyOTPCommandHandler(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task<ResponseModel<Guid>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
