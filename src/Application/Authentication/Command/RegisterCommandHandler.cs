using System;
using System.Net;
using Application.Common;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using MediatR;
using Shared.Common;

namespace Application.Authentication.Command;

public class RegisterCommandHandler : CommonHandler, IRequestHandler<RegisterCommand, ResponseModel>
{
    public RegisterCommandHandler(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = await this._dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                if (request == null)
                    return this.FailResponse(HttpStatusCode.BadRequest, "20001");

                if (request.user == null || string.IsNullOrWhiteSpace(request.user.username))
                    return this.FailResponse(HttpStatusCode.BadRequest, "20002");

                var code = new UserCodeValueObject("000001");
                var username = new UsernameValueObject(request.user.username);
                var authenticationType = new AuthenticationTypeValueObject("PASSWORD");

                var user = new UserEntity(code: code, username: username, authenticationType: authenticationType);

                if (request.userAuthentication == null || string.IsNullOrWhiteSpace(request.userAuthentication.password) || string.IsNullOrWhiteSpace(request.userAuthentication.confirmPassword))
                    return this.FailResponse(HttpStatusCode.BadRequest, "20003");

                if (request.userAuthentication.password == request.userAuthentication.confirmPassword)
                    return this.FailResponse(HttpStatusCode.BadRequest, "20004");

                var password = new PasswordValueObject(request.userAuthentication.password);

                var algorithm = new AlgorithmValueObject(Guid.NewGuid(), "{salt}_{plaintext}_{key}_{iv}", "KEY", "IV", "SALT");
                var passwordEncrypted = algorithm.GenerateAlgorithmInput(password.ToString());
                password.EncryptWithKey(algorithm.keyBytes!, algorithm.ivBytes!);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                await transaction.RollbackAsync();

                return this.FailResponse(HttpStatusCode.UnprocessableEntity, "20000");
            }

            return default!;
        }
    }
}
