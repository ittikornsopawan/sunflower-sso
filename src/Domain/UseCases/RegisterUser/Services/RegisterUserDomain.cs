using System;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.UseCases.RegisterUser.Services;

public interface IRegisterUserDomain
{
    Task Register(UserEntity entity);
}

public class RegisterUserDomain : IRegisterUserDomain
{
    public RegisterUserDomain()
    {

    }

    public async Task Register(UserEntity entity)
    {
    }
}
