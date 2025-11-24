using System;
using Domain.ValueObjects;

namespace Domain.UseCases.RegisterUser.Services;

public class RegisterUserDomain
{
    public RegisterUserDomain()
    {

    }

    public async Task Register(UsernameValueObject username, PasswordValueObject password)
    {
    }
}
