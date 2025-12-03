using System;
using System.Net;
using Application.Common;
using Domain.Entities;
using Domain.UseCases.Key.Services;
using Domain.UseCases.RegisterUser.Services;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using MediatR;
using Shared.Common;

namespace Application.Authentication.Command;

public class RegisterCommandHandler : CommonHandler, IRequestHandler<RegisterCommand, ResponseModel>
{
    private IUserCodeGeneratorDomain _userCodeGeneratordomain;
    private IRegisterUserDomain _registerUserDomain;
    private IAlgorithmKeyDomain _algorithmKeyDomain;
    public RegisterCommandHandler(AppDbContext dbContext, IUserCodeGeneratorDomain userCodeGeneratordomain, IRegisterUserDomain registerUserDomain, IAlgorithmKeyDomain algorithmKeyDomain) : base(dbContext)
    {
        this._userCodeGeneratordomain = userCodeGeneratordomain;
        this._registerUserDomain = registerUserDomain;
        this._algorithmKeyDomain = algorithmKeyDomain;
    }

    public async Task<ResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = await this._dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                if (request == null) return this.FailResponse(HttpStatusCode.BadRequest, "20001");

                if (request.user == null || string.IsNullOrWhiteSpace(request.user.username)) return this.FailResponse(HttpStatusCode.BadRequest, "20002");

                var userCode = await this._userCodeGeneratordomain.GenerateAsync();

                var userId = new UserIdValueObject();
                var code = new UserCodeValueObject(userCode);
                var username = new UsernameValueObject(request.user.username);
                var authenticationType = new AuthenticationTypeValueObject("PASSWORD");

                var user = new UserEntity(userId: userId, code: code, username: username, authenticationType: authenticationType);
                await this._registerUserDomain.Register(user);

                if (request.userAuthentication == null || string.IsNullOrWhiteSpace(request.userAuthentication.password) || string.IsNullOrWhiteSpace(request.userAuthentication.confirmPassword))
                    return this.FailResponse(HttpStatusCode.BadRequest, "20003");

                if (request.userAuthentication.password == request.userAuthentication.confirmPassword)
                    return this.FailResponse(HttpStatusCode.BadRequest, "20004");

                var filterNames = new List<string> { "AES-256: CBC-PKCS7" };
                var defaultAlgorithms = await this._algorithmKeyDomain.Get(filterNames);
                var defaultAlgorithm = defaultAlgorithms.First();



                var algorithm = new AlgorithmValueObject(
                    Guid.NewGuid(),
                    "{salt}_{plaintext}_{key}_{iv}",
                    "KEY",
                    "IV",
                    "SALT"
                );
                var password = new PasswordValueObject(request.userAuthentication.password);

                var passwordEncrypted = algorithm.GenerateAlgorithmInput(password.ToString());
                password.EncryptWithKey(algorithm.keyBytes!, algorithm.ivBytes!);

                await transaction.CommitAsync();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex}");
                await transaction.RollbackAsync();

                return this.FailResponse(HttpStatusCode.UnprocessableEntity, "30002");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex}");
                await transaction.RollbackAsync();

                return this.FailResponse(HttpStatusCode.UnprocessableEntity, "30001");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                await transaction.RollbackAsync();

                return this.FailResponse(HttpStatusCode.UnprocessableEntity, "30000");
            }

            return default!;
        }
    }
}
